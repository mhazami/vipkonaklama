using System;
using Radyn.Utility;
using Radyn.WebDesign.DataStructure;

namespace Radyn.WebDesign.Definition
{

    public static class Extention
    {
      
        public static string GetWebSiteCompleteUrl(this WebSite homa)
        {
            return System.Web.HttpContext.Current != null ? (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Url.Authority) ? "Http://" + System.Web.HttpContext.Current.Request.Url.Authority : "") : "";
        }



        public static string FillTempAdditionalData(string payType, byte? bankId, string paymentUrl, int terminal, string username, string password, string certificatePath, string certificatePassword, string merchantId, string merchantPublicKey, string merchantPrivateKey)
        {
            var textToBeDecrypted = payType + "," + (bankId != null ? bankId.ToString() : String.Empty) + "," + paymentUrl + "," + terminal + "," + username + "," + password + "," + (certificatePath ?? String.Empty) + "," + (certificatePassword ?? String.Empty) + "," + (merchantId ?? String.Empty) + "," + (merchantPublicKey ?? String.Empty) + "," + (merchantPrivateKey ?? String.Empty);
            return StringUtils.Encrypt(textToBeDecrypted);
        }






    }
}
