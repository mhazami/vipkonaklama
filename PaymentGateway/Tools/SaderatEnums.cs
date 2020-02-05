using System.ComponentModel;

namespace Radyn.PaymentGateway.Tools
{
    public class SaderatEnums
    {
        public enum resultCode
        {
            [Description("موفقیت در تراکنش")]
            Succed = 100,
            [Description("انصراف دهنده کارت")]
            Cancelthecard = 110,
            [Description("موجودی حساب کافی نیست")]
            Accountsisnotenough = 120,
            [Description("اطلاعات کارت صحیح نمیباشد")]
            Cardinformationisnotcorrect = 130,
            [Description("رمز کارت اشتباه است")]
            Passwordiswrongcard = 131,
            [Description("کارت مسدود میباشد")]
            Cardisblocked = 132,
            [Description("کارت منقضی شده است")]
            CardisExpired = 133,
            [Description("زمان مورد نظر به پایان رسیده است")]
            Thetimehasexpired = 140,
            [Description("خطای داخلی بانک")]
            ErrorInBank = 150,
            [Description("یا ExpireDate cvv2 خطا در اطلاعات")]
            ErrorInCvv2 = 160,
            [Description("بانک صادر کننده کارت شما مجور انجام تراکنش را صادر نکرده است")]
            YourbankcardissuerhasNotauthorizedthetransaction = 166,
            [Description("مبلغ تراکنش بیشتر از سقف مجاز برای هر تراکنش میباشد")]
            Transactionamountisgreaterthanthemaximumallowedforeachtransaction = 200,
            [Description("مبلغ تراکنش بیشتر از سقف مجاز در روز میباشد")]
            TransactionamountisgreaterthanthemaximumallowedforeachtransactionInDay = 201,
            [Description("مبلغ تراکنش بیشتر از سقف مجاز در ماه میباشد")]
            TransactionamountisgreaterthanthemaximumallowedforeachtransactionInMoth = 202,
            [Description("ترامنش با موفقیت انجام شد")]
            SuccedVerify = 0,
            [Description("وجود کاراکتر های غیر مجاز در درخواست")]
            Charactersareallowedonrequest = -20,
            [Description("تراکنش قبلا برگشت خورده است")]
            Transactionhasalreadyreturned = -30,
            [Description("طول رشته درخواست غیر مجاز است")]
            Lengthoftherequestisinvalid = -50,
            [Description("خطا در درخواست")]
            ErrorInRequesrt = -51,
            [Description("تراکنش مورد نظر یافت نشد")]
            TransactionNotFound = -80,
            [Description("خطای داخلی بانک")]
            ErrorInBankInVerify = -81,
            [Description("تراکنش قبلا تایید شده است")]
            Transactionhasalreadybeenapproved = -90,


        }
       
    }
}
