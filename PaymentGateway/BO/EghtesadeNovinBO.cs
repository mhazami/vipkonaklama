using System;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using Radyn.Payment;
using Radyn.Payment.DataStructure;
using Radyn.PaymentGateway.Tools;
using Radyn.Utility;

namespace Radyn.PaymentGateway.BO
{
    public class EghtesadeNovinBO
    {
        private static EghtesadeNovinGeteway.ServiceGeteway _eghtesadeNovinGateway;
        internal static EghtesadeNovinGeteway.ServiceGeteway EghNovinGateway
        {
            get { return _eghtesadeNovinGateway ?? new EghtesadeNovinGeteway.ServiceGeteway(); }
        }

        public string EghtesadeNovinCallPayRequest(Transaction transaction, string reservationNum, string username, string passwordEncrypted, string requestAuthority, string certificatePath, string certificatePasswordEncrypted)
        {
            //string resNum = null;//شماره فاکتور فروشنده
            var amount = (int)transaction.Amount; //مبلغ تراکنش
            //string redirectUrl = HttpUtility.HtmlEncode(requestAuthority); //آدرس سایت فروشنده در بازگشت
            string redirectUrl = HttpUtility.HtmlEncode(Enums.Bank.EghtesadeNovin.AfterCallBackUrl(transaction.Id, requestAuthority)); //آدرس سایت فروشنده در بازگشت

            try
            {
                #region Login

                var modernService = new EghtesadeNovinGeteway.ServiceGeteway();
                modernService.AllowAutoRedirect = true;
                var req = new EghtesadeNovinGeteway.loginRequest();

                var passwordDecrypt = StringUtils.Decrypt(passwordEncrypted);
                req.username = username;
                req.password = passwordDecrypt;

                string sessionId = modernService.login(req);

                var context = new EghtesadeNovinGeteway.wsContext() {data = new EghtesadeNovinGeteway.wsContextEntry[1]};
                context.data[0] = new EghtesadeNovinGeteway.wsContextEntry();
                context.data[0].key = "SESSION_ID";
                context.data[0].value = sessionId;

                #endregion

                try
                {
                    #region Get Sign Code

                    var dataSgn = new EghtesadeNovinGeteway.dataToSignResponse();
                    dataSgn = modernService.getPurchaseParamsToSign(reservationNum, amount, true, redirectUrl);

                    var decryptedPass = StringUtils.Decrypt(certificatePasswordEncrypted);

                 
                 
                    Sign sign = new Sign(certificatePath, decryptedPass);
                    byte[] signedBytes = sign.SignSomeText(dataSgn.dataToSign);
                    string signature = System.Convert.ToBase64String(signedBytes);

                    #endregion

                    try
                    {
                        #region Get Token Key

                        var tknInfo = new EghtesadeNovinGeteway.tokenInfo();
                        tknInfo = modernService.generateSignedPurchaseToken(context, signature, dataSgn.uniqueId,
                            reservationNum, amount, true, redirectUrl);

                        #endregion

                        try
                        {
                            #region Send Token To Bank

                            string token = tknInfo.token;
                            DateTime expirationdate = tknInfo.expirationDate;

                            var data = string.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}#", token, username, passwordEncrypted,
                                reservationNum, transaction.InvoiceId, (long) transaction.Amount, redirectUrl);
                            var callBankUrl = Enums.Bank.EghtesadeNovin.CallBankUrl(transaction.Id, requestAuthority);
                            transaction.AdditionalData = StringUtils.Encrypt(data);
                            return !PaymentComponenets.Instance.TransactionFacade.Update(transaction)
                                ? string.Empty
                                : callBankUrl;

                            #endregion
                        }

                        catch (Exception ex)
                        {
                            Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);

                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);

                    }
                }
                catch (Exception ex)
                {
                    Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);

                }
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
            }

            return null;

            //---------------------------------------
            //var data = string.Format("{0}#{1}#{2}#{3}#{4}#{5}#", username, terminalId, password, transaction.InvoiceId,
            //     (long)transaction.Amount,
            //    Enums.Bank.Saman.AfterCallBackUrl(transaction.Id, requestAuthority));
            //transaction.AdditionalData = StringUtils.Encrypt(data);
            //var radynCallPayRequestInRadyn = Enums.Bank.Saman.CallBankUrl(transaction.Id, requestAuthority);
            //return !PaymentComponenets.Instance.TransactionFacade.Update(transaction) ? string.Empty : radynCallPayRequestInRadyn;
        }

        internal Transaction EghtesadeNovinCallBackPayRequest(Guid id, string token, string MID, string resNum, string refNum, string state, string language, string cardPanHash)
        {
            if (state == null)
            {
                throw new Exception("وضعیت تراکنش نامشخص است.");
            }

            if (state != "OK")
            {
                throw new Exception(EghtesadeNovinEnums.StatusList[state]);
            }
            if (string.IsNullOrEmpty(refNum))
            {
                throw new Exception("گويا خريد شما توسط بانک تاييد شده است اما رسيد ديجيتالي شما تاييد نگشت");
            }

            var transaction = PaymentComponenets.Instance.TransactionFacade.Get(id);
            if (transaction == null)
            {
                return null;
            }

            try
            {
                #region Login To Bank Server

                var additionalData = !string.IsNullOrEmpty(transaction.AdditionalData)
                    ? StringUtils.Decrypt(transaction.AdditionalData)
                    : "";
                var data = additionalData.Split('#');

                var username = data[1];
                var password = StringUtils.Decrypt(data[2]);
                var amount = (int) transaction.Amount;

                var modernService = new EghtesadeNovinGeteway.ServiceGeteway();
                modernService.AllowAutoRedirect = true;
                var req = new EghtesadeNovinGeteway.loginRequest();
                req.username = username;
                req.password = password;


                string sessionId = modernService.login(req);

                var context = new EghtesadeNovinGeteway.wsContext() {data = new EghtesadeNovinGeteway.wsContextEntry[1]};
                context.data[0] = new EghtesadeNovinGeteway.wsContextEntry();
                context.data[0].key = "SESSION_ID";
                context.data[0].value = sessionId;

                #endregion

                try
                {
                    #region Verify Transaction to Bank

                    var tknRqst = new EghtesadeNovinGeteway.tokenPurchaseVerificationRequest();

                    tknRqst.amount = amount; //amount
                    tknRqst.token = token;
                    tknRqst.referenceNumber = refNum;
                    tknRqst.amountSpecified = true;

                    var tknRsns = new EghtesadeNovinGeteway.tokenPurchaseVerificationResponse();
                    //if don't verify transaction the amount back to buyer account after 30 min
                    tknRsns = modernService.tokenPurchaseVerifyTransaction(context, tknRqst);
                    decimal bankAmount = tknRsns.resultTotalAmount;

                    #endregion

                    #region Update "Transaction" Object in Db

                    transaction.Status = (int) EghtesadeNovinEnums.Status.SUCCESS;
                    transaction.RefId = refNum;
                    transaction.Done = true;
                    //transaction.SaleReferenceId = refNum.ToLong();
                    if (!PaymentComponenets.Instance.TransactionFacade.Update(transaction)) return null;
                    return transaction;

                    #endregion
                }

                catch (Exception ex)
                {
                    Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);

                }

                modernService.logout(context);
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);

            }


            return transaction;



            //----------------------------------------

            //var transaction = PaymentComponenets.Instance.TransactionFacade.Get(Id);
            //if (transaction == null)
            //    return null;
            //if (status != "OK")
            //    throw new Exception(SamanEnums.StatusList[status]);
            //if (string.IsNullOrEmpty(refNum))
            //    throw new Exception("گويا خريد شما توسط بانک تاييد شده است اما رسيد ديجيتالي شما تاييد نگشت");
            //var value = !string.IsNullOrEmpty(transaction.AdditionalData) ? StringUtils.Decrypt(transaction.AdditionalData) : "";
            //var data = value.Split('#');
            //var result = EghNovinGateway.verifyTransaction(refNum, data[1]);
            //if (result > 0)
            //{
            //    if (result < transaction.Amount.ToString().ToDouble())
            //    {
            //        transaction.Status = (int?)SamanEnums.Status.OkSmaller;
            //    }
            //    else
            //    {
            //        if (result.Equals(transaction.Amount.ToString().ToDouble()))
            //        {
            //            transaction.Status = (int?)SamanEnums.Status.Ok;
            //            transaction.Done = true;
            //        }
            //        else if (result > transaction.Amount.ToString().ToDouble())
            //        {
            //            transaction.Done = true;
            //            transaction.Status = (int?)SamanEnums.Status.OkUpper;
            //        }
            //    }
            //}
            //else
            //{
            //    transaction.Status = (int?)result.ToString().ToEnum<SamanEnums.Status>();
            //}
            //transaction.RefId = refNum;
            //transaction.SaleReferenceId = traceNo.ToLong();
            //if (!PaymentComponenets.Instance.TransactionFacade.Update(transaction)) return null;
            //return transaction;
        }

        class Sign
        {
            private string CertificatePath { get; set; }
            private string CertificatePass { get; set; }

            public Sign(string certPath, string certPass)
            {
                this.CertificatePath = certPath;
                this.CertificatePass = certPass;
            }

            //const string subject = "محمدرضا";//certificate name ;

            public byte[] SignSomeText(string message)
            {
                Encoding unicode = Encoding.UTF8;
                byte[] msgBytes = Encoding.Default.GetBytes(message);
                X509Certificate2 signerCert = GetSignerCert();
                byte[] encodedSignedCms = SignMsg(msgBytes, signerCert);
                return encodedSignedCms;
            }

            private X509Certificate2 GetSignerCert()
            {
                //string certPath = @"C:\Users\Radyn7\Downloads\Compressed\olumdaruei-certificate\olumdaruei.p12"; //.p12 certificate path  ;
                //string certPath =// Server.MapPath("/")// System.Web.map;
                //string certPass = "olumdaruei@253379"; //certificate password
                X509Certificate2Collection collection = new X509Certificate2Collection();
                collection.Import(CertificatePath, CertificatePass, X509KeyStorageFlags.PersistKeySet);
                foreach (X509Certificate2 cert in collection)
                {
                    return cert;
                    //if (cert.Subject.ToLower().Contains(CertificateSubject.ToLower()))
                    //{
                    //    return cert;
                    //}
                }
                return null;
            }

            //  Sign the message by the using the private key of the signer.
            //  Note that signer's public key certificate is input here 
            //  because it is used to locate the corresponding private key.
            private byte[] SignMsg(Byte[] msg, X509Certificate2 signerCert)
            {
                ContentInfo contentInfo = new ContentInfo(msg);
                SignedCms signedCms = new SignedCms(contentInfo);
                CmsSigner cmsSigner = new CmsSigner(signerCert);
                cmsSigner.IncludeOption=X509IncludeOption.EndCertOnly;
                cmsSigner.SignedAttributes.Add(new Pkcs9SigningTime());
                signedCms.ComputeSignature(cmsSigner);
                return signedCms.Encode();
            }
        }
    }
}
