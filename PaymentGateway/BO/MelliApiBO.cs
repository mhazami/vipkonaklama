using System;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Radyn.Framework;
using Radyn.Payment;
using Radyn.Payment.DataStructure;
using Radyn.PaymentGateway.Tools;
using Radyn.PaymentGateway.Tools.ViewModels;
using Radyn.Utility;

namespace Radyn.PaymentGateway.BO
{
    public class MelliApiBO
    {
        public string MelliCallPayRequest(Transaction transaction, string terminalId, string merchantId, string merchantKey, string requestAuthority)
        {
            string radynCallPayRequestInRadyn = "";

            merchantKey = StringUtils.Decrypt(merchantKey).Trim();
            var symmetric = SymmetricAlgorithm.Create("TripleDes");
            symmetric.Mode = CipherMode.ECB;
            symmetric.Padding = PaddingMode.PKCS7;
            var dataBytes = Encoding.UTF8.GetBytes(string.Format("{0};{1};{2}", terminalId, transaction.InvoiceId, (long)transaction.Amount));
            var encryptor = symmetric.CreateEncryptor(System.Convert.FromBase64String(merchantKey), new byte[8]);
            var signData = System.Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));

            var data = $"{signData}#{terminalId}#{merchantId}#{merchantKey}#{transaction.InvoiceId}#{Enums.Bank.Melli.AfterCallBackUrl(transaction.Id, requestAuthority)}";
            transaction.AdditionalData = data;
            var res = MelliCalpayRequest(transaction, requestAuthority);

            if (res != null)
            {
                var outstr = res.ResCode;
                transaction.Status = (int?)MelliEnums.AppStatusCode.WaitStatus;
                transaction.RefId = outstr;
                if (outstr == "0")
                {
                    data = $"{signData}#{terminalId}#{merchantId}#{merchantKey}#{transaction.InvoiceId}#{Enums.Bank.Melli.AfterCallBackUrl(transaction.Id, requestAuthority)}#{res.Token}";
                }
                else
                {
                    throw new Exception(res.Description);
                }
                transaction.AdditionalData = data;
                radynCallPayRequestInRadyn = Enums.Bank.Melli.CallBankUrl(transaction.Id, requestAuthority);
            }
            else radynCallPayRequestInRadyn = Enums.Bank.Melli.AfterCallBackUrl(transaction.Id, requestAuthority);
            if (!PaymentComponenets.Instance.TransactionFacade.Update(transaction)) return string.Empty;

            return radynCallPayRequestInRadyn;
        }

        public PayResultData MelliCalpayRequest(Transaction transaction, string requestAuthority)
        {
            var str = StringUtils.Decrypt(transaction.AdditionalData);
            var strterminal = str.Split('#');
            var terminalId = strterminal[1];
            var merchantId = strterminal[2];
            var signData = strterminal[0];

            var data = new
            {
                TerminalId = terminalId,
                MerchantId = merchantId,
                Amount = (long)transaction.Amount,
                SignData = signData,
                ReturnUrl = Enums.Bank.Melli.AfterCallBackUrl(transaction.Id, requestAuthority),
                LocalDateTime = DateTime.Now,
                OrderId = transaction.InvoiceId.ToString(),
            };

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://sadad.shaparak.ir/VPG/api/v0/Request/PaymentRequest");
                client.DefaultRequestHeaders.Accept.Clear();
                var w = client.PostAsJsonAsync("https://sadad.shaparak.ir/VPG/api/v0/Request/PaymentRequest", data);
                w.Wait();
                HttpResponseMessage response = w.Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<PayResultData>();
                    result.Wait();
                    return result.Result;
                }
                else
                {
                    throw new KnownException("");
                }
            }
        }

        public Transaction MelliCallBackPayRequest(Guid Id, string tk)
        {
            var transaction = PaymentComponenets.Instance.TransactionFacade.Get(Id);
            var str = StringUtils.Decrypt(transaction.AdditionalData);
            var strterminal = str.Split('#');
            //var terminalId = strterminal[1];
            var merchantKey = StringUtils.Decrypt(strterminal[3]).Trim();

            var dataBytes = Encoding.UTF8.GetBytes(tk);
            var symmetric = SymmetricAlgorithm.Create("TripleDes");
            symmetric.Mode = CipherMode.ECB;
            symmetric.Padding = PaddingMode.PKCS7;
            var encryptor = symmetric.CreateEncryptor(System.Convert.FromBase64String(merchantKey), new byte[8]);
            var signedData = System.Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));

            //var dataBytes = Encoding.UTF8.GetBytes(string.Format("{0};{1};{2}", terminalId, transaction.InvoiceId, (long)transaction.Amount));
            //var encryptor = symmetric.CreateEncryptor(System.Convert.FromBase64String(merchantKey), new byte[8]);
            //var signData = System.Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));

            var data = new
            {
                token = tk,
                SignData = signedData
            };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://sadad.shaparak.ir/VPG/api/v0/Advice/Verify");
                client.DefaultRequestHeaders.Accept.Clear();
                var w = client.PostAsJsonAsync("https://sadad.shaparak.ir/VPG/api/v0/Advice/Verify", data);
                w.Wait();
                HttpResponseMessage response = w.Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<VerifyResultData>();
                    result.Wait();

                    if (result != null && result.Result != null)
                    {
                        transaction.Status = int.Parse(result.Result.ResCode);
                        //transaction.SaleReferenceId = long.Parse(result.Result.SystemTraceNo);
                        transaction.DocNo = result.Result.SystemTraceNo + "#" + result.Result.Description;
                        transaction.RefId = result.Result.RetrivalRefNo;
                        if (result.Result.ResCode == "0")
                        {
                            result.Result.Succeed = true;
                            transaction.Done = true;
                            transaction.SaleReferenceId = long.Parse(result.Result.SystemTraceNo);
                        }
                    }
                }
                else
                {
                    throw new KnownException(" خطا در تراکنش");
                }
            }
            if (!PaymentComponenets.Instance.TransactionFacade.Update(transaction)) return null;
            return transaction;
        }

    }
}
