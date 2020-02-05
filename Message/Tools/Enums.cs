using Radyn.Framework;

namespace Radyn.Message.Tools
{
    public class Enums
    {
        public enum DeliveredType
        {
            [System.ComponentModel.Description("پیام ارسال شد ")]
            Unknown = 0,
            [System.ComponentModel.Description("پیام با موفقیت ارسال شد")]
            Delivered = 1,
            [System.ComponentModel.Description("پیام ارسال نشد")]
            NotDelivered = 2,
            [System.ComponentModel.Description("مشکلی در ارتباط با مخابرات وجود دارد")]
            NotRecivedContacts = 3,
        }
        public enum SentMailUserType
        {
            To = 1,
            CC = 2,
            BCC = 3
        }

        public enum MailSection : byte
        {
            [Description("Compose", Type = typeof(Resources.Message))]
            Compose = 1,
            [Description("Inbox", Type = typeof(Resources.Message))]
            Inbox = 2,
            [Description("OutBox", Type = typeof(Resources.Message))]
            OutBox = 3,
            [Description("Draft", Type = typeof(Resources.Message))]
            Draft = 4,
            [Description("Trash", Type = typeof(Resources.Message))]
            Trash = 5,
         
        }
    }
}
