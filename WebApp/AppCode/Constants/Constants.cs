using System.Text;

namespace Radyn.WebApp.AppCode.Constants
{

    public class Constants
    {

        public const string WebDesignOperationId = "349883c3-5fc3-4e4a-bf41-8da6be7f8e61";
        public const string PhoneBookOperationId = "4207863D-B2EE-4ED0-BCF5-EF83A9D55D03";

        public static string Errorbox(string message)
        {
            StringBuilder str = new StringBuilder();
            str.Append($"<div class=\"div-error\">");
            str.Append("<span class=\"btn-close\" onclick=\"closeErrorBox(this)\"><i class=\"fa fa-close\"></i></span>");
            str.Append($"<span class=\"txt-message\">{message}</span>");
            str.Append("</div>");
            return str.ToString();
        }
    }


}