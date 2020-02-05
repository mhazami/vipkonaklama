using System.Web.Mvc;
using Radyn.Common;
using Radyn.News;
using Radyn.News.DataStructure;
using Radyn.News.Tools;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.News.Controllers
{
    public class NewsContentController : WebDesignBaseController
    {
        public ActionResult Modify(string state, int newsid)
        {
            switch (state)
            {
                case "Modify":
                    {
                        var newcontent = new NewsContent() { LanguageId = SessionParameters.Culture };
                        if (newsid != 0)
                        {
                            var content = NewsComponent.Instance.NewsFacade.Get(newsid);
                            newcontent = content.GetNewsContent(SessionParameters.Culture);
                        }
                        ViewBag.Lanuages = new SelectList(CommonComponent.Instance.LanguageFacade.Where(x=>x.Enabled), "Id", "DisplayName");
                        return PartialView("Modify", newcontent);
                    }

                case "Details":
                    {
                        var content = NewsComponent.Instance.NewsFacade.Get(newsid);
                        return PartialView("DetailInfo", content.GetNewsContent(SessionParameters.Culture));
                    }
            }

            return null;
        }
        public ActionResult ChangeCulture(string langid, int newsid)
        {

            if (!string.IsNullOrEmpty(langid))
            {
                var contenet = new NewsContent() { LanguageId = langid };
                if (newsid != 0)
                {
                    var itemContent = NewsComponent.Instance.NewsContentFacade.Get(newsid, langid);
                    if (itemContent != null) contenet = itemContent;
                }
                ViewBag.Lanuages = new SelectList(CommonComponent.Instance.LanguageFacade.Where(x=>x.Enabled), "Id", "DisplayName");
                return PartialView("Modify", contenet);
            }
            return null;

        }





    }
}
