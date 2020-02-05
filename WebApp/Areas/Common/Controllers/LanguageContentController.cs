using System.Web.Mvc;
using Radyn.Common;
using Radyn.WebApp.AppCode.Base;

namespace Radyn.WebApp.Areas.Common.Controllers
{
    public class LanguageContentController : WebDesignBaseController
    {
        public ActionResult ChangeCulture(string key, string langid)
        {
            if (string.IsNullOrEmpty(langid)) return null;
            var contenet = CommonComponent.Instance.LanguageContentFacade.Get(key, langid);
            return Content(contenet!=null ? contenet.Value : " ");
        }
       

    }
}