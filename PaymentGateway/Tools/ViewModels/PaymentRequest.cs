using System;

namespace Radyn.PaymentGateway.Tools.ViewModels
{
    public class PaymentRequest
    {
        public PaymentRequest()
        {
        }

        public string TerminalId { get; set; }
        public string MerchantId { get; set; }

        public long Amount { get; set; }
        public string OrderId { get; set; }
        public string AdditionalData { get; set; }
        public DateTime LocalDateTime { get; set; }
        public string ReturnUrl { get; set; }
        public string SignData { get; set; }
        public bool EnableMultiplexing { get; set; }
        public string MerchantKey { get; set; }

        public string PurchasePage { get; set; }
    }


    public class PayResultData
    {
        public string ResCode { get; set; }
        public string Token { get; set; }
        public string Description { get; set; }
    }


    public class PurchaseResult
    {
        public string OrderId { get; set; }
        public string Token { get; set; }
        public string ResCode { get; set; }
        public VerifyResultData VerifyResultData { get; set; }
    }

    public class VerifyResultData
    {
        public bool Succeed { get; set; }
        public string ResCode { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public string RetrivalRefNo { get; set; }
        public string SystemTraceNo { get; set; }
        public string OrderId { get; set; }
    }
}
