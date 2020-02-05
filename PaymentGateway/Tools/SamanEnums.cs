using System.Collections.Generic;
using System.ComponentModel;

namespace Radyn.PaymentGateway.Tools
{
    public class SamanEnums
    {
      

       

        public static Dictionary<string, string> StatusList
        {
            get
            {
               var dictionary= new Dictionary<string, string>
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


        public enum Status:int
        {
            [Description("بانک صحت رسيد ديجيتالي شما را تصديق نمود. فرايند خريد تکميل گشت")]
            Ok = 1,
            [Description("خريد شما تاييد و نهايي گشت اما مبلغ انتقالي  بيش از مبلغ خريد ميباشد")]
            OkUpper = 2,
            [Description("مبلغ انتقالي کمتر از مبلغ کل فاکتور ميباشد")]
            OkSmaller = 3,
            [Description("خطای در پردازش اطلاعات ارسالی )مشکل در یکی از ورودی ها و ناموفق بودن فراخوانی متد برگشت تراکنش")]
            Error1 = -1,
            [Description("ورودی ها حاوی کارکترهای ریرمجاز می باشند.")]
            Error2 = -3,
            [Description("Merchant Authentication Failed ) کلمه عبور یا کد فروشنده اشتباه است(.")]
            Error3 = -4,
            [Description("سند قبلا برگشت کامل یافته است.")]
            Error4 = -6,
            [Description("رسید دیجیتالی تهی است.")]
            Error5 = -7,
            [Description("طول ورودی ها بیشتر از حد مجاز است")]
            Error6 = -8,
            [Description("وجود کارکترهای ریرمجاز در مبلخ برگشتی.")]
            Error7 = -9,
            [Description("رسید دیجیتالی به صورت Base64 نیست )حاوی کاراکترهای غیر مجاز است(")]
            Error8 = -10,
            [Description("طول ورودی ها کمتر از حد مجاز است")]
            Error9 = -11,
            [Description("مبلخ برگشتی منفی است.")]
            Error10 = -12,
            [Description("مبلخ برگشتی برای برگشت جزئی بیش از مبلخ برگشت نخورده ی رسید دیجیتالی است.")]
            Error11 = -13,
            [Description("چنین تراکنشی تعریف نشده است.")]
            Error12 = -14,
            [Description("مبلخ برگشتی به صورت اعشاری داده شده است.")]
            Error13 = -15,
            [Description("خطای داخلی سیستم")]
            Error14 = -16,
            [Description("برگشت زدن جزیی تراکنش مجاز نتی باشد.")]
            Error15= -17,
            [Description("IP Address فروشنده نا معتبر است.")]
            Error16= -18,
            


        }
       
    }
}
