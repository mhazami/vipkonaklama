namespace Radyn.PaymentGateway.Tools
{
    public class MellatEnums
    {
        public enum StatusEnums
        {
            [System.ComponentModel.Description("تراکنش با موفقیت ثبت شد")]
            TransactionSucced = 0,

            [System.ComponentModel.Description("شماره کارت معتبر نیست")]
            CardNumberNotValid = 11
            ,
            [System.ComponentModel.Description("موجودی کافی نیست")]
            NotEnoughBlance = 12
            ,
            [System.ComponentModel.Description("رمز دوم شما صحیح نیست")]

            SecondYourPasswordIsIncorrect = 13
            ,
            [System.ComponentModel.Description("دفعات مجاز ورود رمز بیش از حد است")]
            PasswordAttemptsAllowedToo = 14
            ,
            [System.ComponentModel.Description("کارت معتبر نیست")]
            CardIsNotValid = 15
            ,
            [System.ComponentModel.Description("دفعات برداشت وجه بیش از حد مجاز است")]
            WithdrawalTimesOverLimit = 16,
            [System.ComponentModel.Description("کاربر از انجام تراکنش منصرف شده است")]
            UsersTransactionIsDeterred = 17
            ,
            [System.ComponentModel.Description("تاریخ انقضای کارت گذشته است")]
            CardExpiryDateHasPassed = 18
            ,
            [System.ComponentModel.Description("مبلغ برداشت وجه بیش از حد مجاز است")]
            WithdrawalAmountIsExceeded = 19
            ,
            [System.ComponentModel.Description("صادر کننده کارت نامعتبر است")]
            InvalidCardIssuer = 111
            ,
            [System.ComponentModel.Description("خطای سوییچ صادر کننده کارت")]
            SwitchFaultCardIssue = 112
            ,
            [System.ComponentModel.Description("پاسخی از صادر کننده کارت دریافت نشد")]
            ResponseWasReceivedFromTheCardIssuer = 113
            ,
            [System.ComponentModel.Description("دارنده کارت مجاز به انجام این تراکنش نمی باشد")]
            CardHolderIsNotAuthorizedToperformThisTransaction = 114
            ,
            [System.ComponentModel.Description("پذیرنده معتبر نیست")]
            RecipientIsNotValid = 21
            ,
            [System.ComponentModel.Description("خطای امنیتی رخ داده است")]
            SecurityErrorOccurred = 23
            ,
            [System.ComponentModel.Description("اطلاعات کاربری پذیرنده معتبر نیست")]
            UserInfoRecipientIsNotValid = 24
            ,
            [System.ComponentModel.Description("مبلغ نامعتبر است")]
            TheAmountIsInvalid = 25
            ,
            [System.ComponentModel.Description("پاسخ نامعتبر است")]
            TheAnswerIsInvalid = 31
            ,
            [System.ComponentModel.Description("فرمت اطلاعات وارد شده صحیح نیست")]
            DataFormatNotCorrect = 32
            ,
            [System.ComponentModel.Description("حساب نامعتبر است")]
            InvalidAccount = 33
            ,
            [System.ComponentModel.Description("خطای سیستمی")]
            SystemError = 34
            ,
            [System.ComponentModel.Description("تاریخ نامعتبر است")]
            InvalidDate = 35
            ,
            [System.ComponentModel.Description("شماره درخواست تکراری است")]
            NumberOfDuplicateRequests = 41
            ,
            [System.ComponentModel.Description("تراکنش Sale یافت نشد")]
            SaleTransactionNotFound = 42
            ,
            [System.ComponentModel.Description("قبلا درخواست Verify داده شده است")]
            VerifyTheApplicationHasAlreadyBeen = 43
            ,
            [System.ComponentModel.Description("درخواست Verify یافت نشد")]
            VerifyRequestedCouldNotBeFound = 44
            ,
            [System.ComponentModel.Description("تراکنش Settle شده است")]
            SettleTransactionIs = 45
            ,
            [System.ComponentModel.Description("تراکنش Settle نشده است")]
            SettleTransactionIs1 = 46
            ,
            [System.ComponentModel.Description("تراکنش Settle نشده است")]
            SettleTransactionIs2 = 47
            ,
            [System.ComponentModel.Description("تراکنش Reverse شده است")]
            ReverseTransactionsAre = 48
            ,
            [System.ComponentModel.Description("تراکنش Refund یافت نشد")]
            RefundTransactionsFound = 49
            ,
            [System.ComponentModel.Description("شناسه قبض نادرست است")]
            IDBillIsIncorrect = 412
            ,
            [System.ComponentModel.Description("شناسه پرداخت نادرست است")]
            PaymentIsIncorrect = 413
            ,
            [System.ComponentModel.Description("سازمان صادر کننده قبض معتبر نیست")]
            AgencyThatIssuedTheTicketIsNotValid = 414
            ,
            [System.ComponentModel.Description("خطا در ثبت اطلاعات")]
            ErrorsInRecordingInformation = 415
            ,
            [System.ComponentModel.Description("شناسه پرداخت کننده نامعتبر است")]
            PayerIDIsInvalid = 416
            ,
            [System.ComponentModel.Description("اشکال در تعریف اطلاعات مشتری")]
            DifficultyInDefiningCustomerInformation = 417
            ,
            [System.ComponentModel.Description("اشکال در تعریف اطلاعات مشتری")]
            DifficultyInDefiningCustomerInformation2 = 418
            ,
            [System.ComponentModel.Description("تعداد دفعات ورود اطلاعات بیش از حد مجاز است")]
            CountDataIsExceeded = 419
            ,
            [System.ComponentModel.Description("IP معتبر نیست")]
            IPIsNotValid = 421
            ,
            [System.ComponentModel.Description("تراکنش تکراری است")]
            RepetitiveTransactions3 = 51
            ,
            [System.ComponentModel.Description("تراکنش مرجع موجود نیست")]
            TransactionReferenceNo = 54
            ,
            [System.ComponentModel.Description("تراکنش نامعتبر است")]
            InvalidTransaction = 55
            ,
            [System.ComponentModel.Description("خطا در واریز")]
            ErrorDeposit = 61


        }
    }
}
