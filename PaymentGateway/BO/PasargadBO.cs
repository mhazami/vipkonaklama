using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Radyn.Payment;
using Radyn.Payment.DataStructure;
using Radyn.PaymentGateway.Tools;
using Radyn.Utility;
using Convert = System.Convert;

namespace Radyn.PaymentGateway.BO
{
    public class PasargadBO
    {


        public string PasargadPayRequest(Transaction transaction, string terminalId, string username, string password, string requestAuthority)
        {

            var pass = StringUtils.Decrypt(password);
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(pass);
            var data = string.Format("#{0}#{1}#{2}#{3}#{4}#{5}#{6}#{7}#", username, terminalId, transaction.InvoiceId,
                 DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), (long)transaction.Amount,
                Enums.Bank.Pasargad.AfterCallBackUrl(transaction.Id, requestAuthority), "1003",
                DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            var signMain = rsa.SignData(Encoding.UTF8.GetBytes(data), new SHA1CryptoServiceProvider());
            var sign = Convert.ToBase64String(signMain);
            data += sign + "#" + password;
            transaction.AdditionalData = StringUtils.Encrypt(data);
            var radynCallPayRequestInRadyn = Enums.Bank.Pasargad.CallBankUrl(transaction.Id, requestAuthority);
            return !PaymentComponenets.Instance.TransactionFacade.Update(transaction) ? string.Empty : radynCallPayRequestInRadyn;
        }
        public XmlDocument GetReuqestFromUrl(string url, string value)
        {
            var oXml = new XmlDocument();
            var request = (HttpWebRequest)WebRequest.Create(url);
            byte[] textArray = Encoding.UTF8.GetBytes(value);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = textArray.Length;
            request.GetRequestStream().Write(textArray, 0, textArray.Length);
            var response = (HttpWebResponse)request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();
            oXml.LoadXml(result);
            return oXml;
        }


        public bool PasargadCallBackPayRequest(Transaction transaction)
        {

            if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.QueryString["tref"]))
            {
                var checkTransactionResult = GetReuqestFromUrl("https://pep.shaparak.ir/CheckTransactionResult.aspx", "invoiceUID=" + System.Web.HttpContext.Current.Request.QueryString["tref"]);
                var checkTransactionResultoElResult =
                    (XmlElement)checkTransactionResult.SelectSingleNode("//result"); //نتیجه تراکنش
                transaction.RefId = System.Web.HttpContext.Current.Request.QueryString["tref"];
                if (checkTransactionResultoElResult != null && checkTransactionResultoElResult.InnerText.ToBool())
                {
                    var oElTraceNumber = (XmlElement)checkTransactionResult.SelectSingleNode("//traceNumber");//شماره پیگیری
                    var txNreferenceNumber = (XmlElement)checkTransactionResult.SelectSingleNode("//referenceNumber"); //شماره ارجاع
                    var getinvoiceDate = (XmlElement)checkTransactionResult.SelectSingleNode("//invoiceDate"); //شماره ارجاع
                  

                    if (txNreferenceNumber != null && !String.IsNullOrEmpty(txNreferenceNumber.InnerText))
                        transaction.SaleReferenceId = txNreferenceNumber.InnerText.ToLong();
                    if (oElTraceNumber != null && !String.IsNullOrEmpty(oElTraceNumber.InnerText))
                        transaction.SaleTrackingId = oElTraceNumber.InnerText.ToLong();
                    if (getinvoiceDate != null && !String.IsNullOrEmpty(getinvoiceDate.InnerText))
                        transaction.PayDate = getinvoiceDate.InnerText.ToDateTime();

                    var invoiceDate = getinvoiceDate != null ? getinvoiceDate.InnerText : DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    var timeStamp = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    var additionalData = transaction.AdditionalData;
                    var decrypt = StringUtils.Decrypt(additionalData);
                    var split = decrypt.Split('#');
                    var rsa = new RSACryptoServiceProvider();
                    var xmlString = StringUtils.Decrypt(split[10]);
                    rsa.FromXmlString(xmlString);
                    var merchandcode =split[1];
                    var terminalId =split[2];
                    string data = string.Format("#{0}#{1}#{2}#{3}#{4}#{5}#", merchandcode, terminalId, transaction.InvoiceId, invoiceDate, (long)transaction.Amount, timeStamp);
                    byte[] signedData = rsa.SignData(Encoding.UTF8.GetBytes(data), new
                    SHA1CryptoServiceProvider());
                    string signedString = Convert.ToBase64String(signedData);
                    var text =
                        string.Format(
                            "InvoiceNumber={0}&InvoiceDate={1}&MerchantCode={2}&TerminalCode={3}&Amount={4}&TimeStamp={5}&Sign={6}",
                            transaction.InvoiceId, invoiceDate, merchandcode,
                            terminalId, (long)transaction.Amount, timeStamp,
                            signedString);

                    var verifyPayment = GetReuqestFromUrl("https://pep.shaparak.ir/VerifyPayment.aspx", text);
                    var verifyPaymentoElResult = (XmlElement)verifyPayment.SelectSingleNode("//result");//نتیجه تراکنش
                    var verifyPaymentoElresultMessage = (XmlElement)verifyPayment.SelectSingleNode("//resultMessage");//نتیجه تراکنش
                    transaction.Status = (verifyPaymentoElResult != null && verifyPaymentoElResult.InnerText.ToBool()) ? 0 : (int?)null;
                    transaction.Done = transaction.Status != null && transaction.Status==0;
                    transaction.OnlinePayMessage = verifyPaymentoElresultMessage != null ? verifyPaymentoElresultMessage.InnerText : null;

                }
                else
                {
                    transaction.OnlinePayMessage = "کاربر از انجام تراکنش منصرف شده است ";
                }
            }

            else
            {
                transaction.OnlinePayMessage = "خطایی در انجام تراکنش وجود دارد";
            }

            return PaymentComponenets.Instance.TransactionFacade.Update(transaction);



        }


    }
}
