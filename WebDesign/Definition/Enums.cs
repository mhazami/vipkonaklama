using Radyn.Framework;

namespace Radyn.WebDesign.Definition
{
    public class Enums
    {
        public enum PaymentSection : byte
        {
            
        }
        public enum SubscribeStatus : byte
        {
            None = 0,
            MailSent = 1,
            NotConfirmed = 2,
            Confirmed = 3,
            Registered = 4,
            Deactived = 5,
        }
        public enum ResourceType
        {
            [Description("JSFile", Type = typeof(Resources.WebDesign))]
            JSFile,
            [Description("CssFile", Type = typeof(Resources.WebDesign))]
            CssFile,
            [Description("JSFunction", Type = typeof(Resources.WebDesign))]
            JSFunction,
            [Description("Style", Type = typeof(Resources.WebDesign))]
            Style,
        }
        public enum HasValue : byte
        {
            [Description("All", Type = typeof(Resources.WebDesign))]
            None = 0,
            [Description("Has", Type = typeof(Resources.WebDesign))]
            Has = 1,
            [Description("NoHas", Type = typeof(Resources.WebDesign))]
            NotHas = 2,

        }
        public enum UseLayout : byte
        {
            None = 0,
            [System.ComponentModel.Description("Layout")]
            Layout = 1,
            [System.ComponentModel.Description("UserLayout")]
            UserLayout = 2,
            [System.ComponentModel.Description("LookUpLayout")]
            LookUpLayout = 3,
            [System.ComponentModel.Description("WebDesignUserLayout")]
            WebDesignUserLayout =4,
           

        }
        public enum WebSiteStatus
        {
            NotRegistered,
            NotConfiged,
            Disabled,
            NoProblem
        }

        public enum UserInformType : byte
        {
            [Description("Email", Type = typeof(Resources.WebDesign))]
            Email = 1,
            [Description("SMS", Type = typeof(Resources.WebDesign))]
            SMS = 2,
            [Description("Both", Type = typeof(Resources.WebDesign))]
            Both = 3,
        }
        public enum UserStatus : byte
        {
            None=0,
            [Description("UserPreRegisterStatus", Type = typeof(Resources.WebDesign))]
            PreRegister = 1,
            [Description("UserRegisterStatus", Type = typeof(Resources.WebDesign))]
            Register = 2,
           

        }



    }
}
