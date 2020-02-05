using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Radyn.Payment;
using Radyn.Payment.DataStructure;
using Radyn.PaymentGateway.Tools;
using Radyn.Utility;

namespace Radyn.PaymentGateway.BO
{
    public class MabnaBO
    {
        private static MabnaGetewayToken.Token _mabnaGateway;

        internal static MabnaGetewayToken.Token MabnaGateway
        {
            get { return _mabnaGateway ?? new MabnaGetewayToken.Token(); }
        }

        public string MabnaCallPayRequest(Transaction transaction, string merchantId, string terminalId, string requestAuthority, string merchantPublicKey, string merchantPrivateKey)
        {
            try
            {
                var callbackAddress = Enums.Bank.Mabna.AfterCallBackUrl(transaction.Id, requestAuthority);

                var invoiceId = transaction.InvoiceId.ToString().PadLeft(9, '0');

                string tokenText = System.Convert.ToInt32(transaction.Amount) + "" + invoiceId + "" + merchantId + "" + callbackAddress + "" + terminalId;

                //string base64TokenKey = createSignTextByPrivateKey(tokenText , merchantPrivateKey);

                var tokenObj = new MabnaGetewayToken.tokenDTO()
                {
                    MID = createSignTextByPublickKey(merchantId, merchantPublicKey),
                    CRN = createSignTextByPublickKey(invoiceId, merchantPublicKey),
                    SIGNATURE = createSignTextByPrivateKey(tokenText, merchantPrivateKey),
                    AMOUNT = createSignTextByPublickKey(System.Convert.ToInt32(transaction.Amount).ToString(), merchantPublicKey),
                    REFERALADRESS = createSignTextByPublickKey(callbackAddress, merchantPublicKey),
                    TID = createSignTextByPublickKey(terminalId, merchantPublicKey)
                };
                var reservObj = MabnaGateway.reservation(tokenObj);

                if (reservObj.result != 0)
                {
                    throw new Exception(MabnaEnums.TarnsactionList[reservObj.result.ToString()]);
                }

                var data = string.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}#{7}", merchantId, terminalId, transaction.InvoiceId,
                    (long)transaction.Amount, callbackAddress, reservObj.token, merchantPublicKey ,merchantPrivateKey);

                transaction.AdditionalData = StringUtils.Encrypt(data);
                var radynCallPayRequestInRadyn = Enums.Bank.Mabna.CallBankUrl(transaction.Id, requestAuthority);
                return !PaymentComponenets.Instance.TransactionFacade.Update(transaction)
                    ? string.Empty
                    : radynCallPayRequestInRadyn;
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id">Transaction Id</param>
        /// <param name="resCode">Result Code of Bank</param>
        /// <param name="trn">رسید دیجیتالی خرید که توسط بانک برگردانده شده است</param>
        /// <param name="crn">Invoice Id</param>
        /// <param name="amount">Amount</param>
        /// <returns></returns>
        internal Transaction MabnaCallBackPayRequest(Guid Id, string resCode, string trn, string crn, int amount)
        {
            var transaction = PaymentComponenets.Instance.TransactionFacade.Get(Id);
            if (transaction == null)
            {
                return null;
            }

            #region Confirm Transaction in Bank

            if (resCode == "00")
            {

                var value = !string.IsNullOrEmpty(transaction.AdditionalData)
                        ? StringUtils.Decrypt(transaction.AdditionalData)
                        : "";
                var data = value.Split('#');

                var verifyObj = new MabnaGetewayReference.confirmationDTO();

                var signText = data[0] + "" + trn + "" + crn;
                var merchantPublicKey = data[6];
                var merchantPrivateKey = data[7];

                verifyObj.CRN = createSignTextByPublickKey(crn, merchantPublicKey);
                verifyObj.MID = createSignTextByPublickKey(data[0], merchantPublicKey);
                verifyObj.TRN = createSignTextByPublickKey(trn, merchantPublicKey);
                verifyObj.SIGNATURE = createSignTextByPrivateKey(signText, merchantPrivateKey);


                var verifyService = new MabnaGetewayReference.TransactionReference();
                var confrimResult = verifyService.sendConfirmation(verifyObj);

                if (confrimResult.RESCODE != null && confrimResult.RESCODE.ToString() != "")
                {
                    if (confrimResult.RESCODE == "0" || confrimResult.RESCODE == "101" || confrimResult.RESCODE == "00")
                    {
                        //پرداخت موفق
                        transaction.Done = true;
                    }

                    transaction.Status = confrimResult.RESCODE.ToInt();
                }
            }
            else if (resCode == "200")
            {
                //"تراکنش توسط کاربر لغو گردید!"
                transaction.Status = resCode.ToInt();
            }
            else
            {
                //"خطا در انجام عملیات!"
                transaction.Status = resCode.ToInt();
            }

            #endregion

            transaction.RefId = trn;
            //transaction.SaleReferenceId = orederId;

            if (!PaymentComponenets.Instance.TransactionFacade.Update(transaction)) return null;
            return transaction;
        }

        private string createSignTextByPrivateKey(string text, string privateKey)
        {
            ///For Ignore SSL Error
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            //تولید یک نمونه برای عملیات ایجاد امضا
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            CspParameters _cpsParameter;
            RSACryptoServiceProvider RSAProvider;
            _cpsParameter = new CspParameters();
            _cpsParameter.Flags = CspProviderFlags.UseMachineKeyStore;
            RSAProvider = new RSACryptoServiceProvider(1024, _cpsParameter);
            RSACryptoServiceProvider.UseMachineKeyStore = true; // important for discountasp.net

            //بارگذاری کلید خصوصی
            //var xml = createXmlMerchantKey(privateKey);
            rsa.FromXmlString(privateKey);
            //داده ها ی بر اساس ترتیب خواسته شده از طرف مبنا کارت کد گذاری و امضا می شوند
            byte[] signMain = rsa.SignData(System.Text.Encoding.UTF8.GetBytes(text), new
            SHA1CryptoServiceProvider());
            //نگهداری و تبدیل امضای تولید شده به فرمت مورد نیاز
            var signature = System.Convert.ToBase64String(signMain);
            return signature;

            //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //rsa.FromXmlString(createXmlMerchantKey(merchantKey));
            ////exception throw in under line 
            //byte[] signMain = rsa.SignData(System.Text.Encoding.UTF8.GetBytes(text), new SHA1CryptoServiceProvider());
            //var signature = System.Convert.ToBase64String(signMain);
            //return signature;
        }

        private string createSignTextByPublickKey(string text, string publicKey)
        {
            //تولید یک نمونه برای عملیات رمزنگاری 
            RSACryptoServiceProvider cipher = new RSACryptoServiceProvider();
            //بارگذاری کلید عمومی برای عملیات رمزنگار
            cipher.FromXmlString(publicKey);


            //فرآیند رمزنگاری مبلغ تراکنش
            byte[] data = Encoding.UTF8.GetBytes(text);
            byte[] cipherText = cipher.Encrypt(data, false);
            return System.Convert.ToBase64String(cipherText);
        }

        private string createXmlMerchantKey(string merchantKey)
        {
            var sb = new StringBuilder();

            sb.Append("<RSAKeyValue>");
            sb.Append("<Modulus>");
            sb.Append(merchantKey);
            sb.Append("</Modulus>");
            sb.Append("<Exponent>");
            sb.Append("AQAB");
            sb.Append("</Exponent>");
            sb.Append("</RSAKeyValue>");

            return sb.ToString();
        }
    }
}
