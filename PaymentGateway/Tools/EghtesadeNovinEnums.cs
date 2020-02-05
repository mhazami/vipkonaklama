using System.Collections.Generic;

namespace Radyn.PaymentGateway.Tools
{
    public class EghtesadeNovinEnums
    {

        public static Dictionary<string, string> StatusList
        {
            get
            {
                var dictionary = new Dictionary<string, string>
                {
                    {"OK", "تراکنش با موفقیت انجام شد"},
                    {"Canceled By User", "تراکنش توسط خریدار کنسل شده است"},
                    {"Invalid Amount", "مبلغ سند برگشتی بیشتر از تراکنش اصلی میباشد"},
                    {"Invalid Transaction", "درخواست برگشت تراکنش رسیده است ولی تراکنش اصلی پیدا نمیشود"},
                    {"Invalid Card Number", "شماره کارت اشتباه است"},
                    {"No Such Issuer", "چنین صادر کننده کارت وجود ندارد"},
                    {"Expired Card Pick Up", "تاریخ انقضاء کارت به پایان رسیده است"},
                    {"Allowable PIN Tries Exceeded PickUp", "رمز 3 با اشتباه وارد شده در نتیجه کارت غیر فعال خواهد شد"},
                    {"Incorrect PIN", "خریدار رمز کارت اشتباه وارد کرده است"},
                    {"Exceeds Withdrawal Amount Limit", "مبلغ بیشتر از سقف مجاز برداشت است"},
                    {"Transaction Cannot Be Completed", "تراکنش کامل شده ولی امکان سند خوردن وجود ندارد"},
                    {"Response Received Too Late", "تراکنش در شبکه بانکی Timeout شده است"},
                    {"Suspected Fraud Pick Up", "خریدار فیلد CCV2 یا Expiredate را اشتباه وارد کرده است"},
                    {"No Sufficient Funds", "موجودی حساب خریدار کافی نیست"},
                    {"Issuer Down Slm", "سیستم بانک صادر کننده کارت خریدار در وضعیت عملیاتی نیست"},
                    {"TME Error", "کلیه خطاهای سایر بانکی باعث ایجاد خطا شده است"}
                };
                return dictionary;
            }
        }

        public enum Status
        {

            /// <remarks/>
            SUCCESS = 1,

            /// <remarks/>
            NOT_VERIFIED  = 2,

            /// <remarks/>
            FAILED = 3,
        }
    }
}
