using System.Text;

namespace Radyn.Message.Tools
{
    public static class MessageServiceBody
    {

        public static string GetEmailBody(string subject, string body)
        {




            var builder = new StringBuilder();
            builder.Append("<div style = 'box-sizing: border-box; background: #ddd; text-align: center'>");
            builder.Append(string.Format("<div style = 'width: 500px; display: inline-block; margin: auto; font-family: vazir'>" +
                      "<div style = 'text-align: center; float: right; width: 100%; background: #fff; overflow: hidden'>" +
                      "<div style = 'background: #009688; color: #fff; box-shadow: 1px 1px 4px #6f6f6f; float: right; width: 100%;'>" +
                      "<img src = 'http://radyn.ir/Content/Images/logo/radyn.png' style = 'width: 110px; padding: 10px; height: 110px; margin: 20px; display: inline-block; border-radius: 50%; background: #fff; box-shadow: inset 0px 0px 0px 3px #ffffff, inset 0px 0px 0px 7px #009688; display: inline-block;' />" +
                      "<h3 style = 'display: block; width: 100%; margin: 0 0 23px 0; font-size: 19px; font-weight: 700;'> شرکت فناوری اطلاعات همراه رسپینا رادین </h3>" +
                      "</div>" +
                      "<div style = 'float: right; width: 100%; margin-top: 4px; color: #3e3e3e; padding: 16px 19px; font-size: 18px; box-sizing: border-box; text-align: right;'>" +
                      "<strong style = 'width: 100%; float: right; margin: 11px 0; font-size:16px; text-align: right; font-weight: 700;'>{0}</strong>" +
                      "<p style = 'direction: rtl; white-space: pre-line; font-size: 13px; padding: 0 3px;'>{1}</p>" +
                      "</div>" +
                      "<div style = 'box-sizing: border-box; direction: rtl; float: right; width: 100%; font-size: 15px; padding: 10px 40px; color: #b7b7b7; background: #3e3e3e;'>" +
                      "<p style = 'direction: rtl; text-align: center; font-size: 15px; margin: 3px'>" +
                      "<span style = 'font-size: 12px;'>" +
                      "ایمیل :" +
                      "</span>" +
                      "info@radyn.ir" +
                      "</p>" +
                      "<p style = 'direction: rtl; text-align: center; font-size: 15px; margin: 3px'>" +
                      "<span style = 'font-size: 12px;'>" +
                      "تلفن تماس" +
                      "</span>" +
                      ":66564382 - 021" +
                      "</p>" +
                      "<p style = 'direction: rtl; text-align: center; font-size: 15px; margin: 3px'>" +
                      "<span style = 'font-size: 12px;'>" +
                      "وب سایت:" +
                      "</span>" +
                      "www.radyn.ir" +
                      "</p>" +
                      "<p style = 'direction: rtl; text-align: center; font-size: 12px; margin: 3px'>" +
                      "<span style = 'font-size: 13px;'>" +
                      "آدرس:" +
                      "</span>" +
                      "تهران - خیابان دکتر مرتضی لبافی نژاد - حد فاصل کارگر و جمالزاده - پلاک 322 - واحد 3 - شرکت فناوری اطلاعات همراه رسپینا رادین" +
                      "</p>" +
                      "</div>" +
                      "</div>" +
                      "</div>", subject, body));


            builder.Append("</div>");
            return builder.ToString();






        }

    }
}
