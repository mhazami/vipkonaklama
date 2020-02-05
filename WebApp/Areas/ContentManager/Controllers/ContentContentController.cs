using System.Web.Mvc;
using Radyn.Common;
using Radyn.ContentManager;
using Radyn.ContentManager.DataStructure;
using Radyn.ContentManager.Tools;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.ContentManager.Controllers
{
    public class ContentContentController : WebDesignBaseController
    {

        public ActionResult Modify(string state, int contentId)
        {
            switch (state)
            {
                case "Modify":
                    {
                        var newcontent = new ContentContent() { LanguageId = SessionParameters.Culture };
                        if (contentId != 0)
                        {
                            var content = ContentManagerComponent.Instance.ContentFacade.Get(contentId);
                            if (content != null)
                                newcontent = content.GetContent(SessionParameters.Culture);
                        }
                        ViewBag.Lanuages = new SelectList(CommonComponent.Instance.LanguageFacade.Where(x=>x.Enabled), "Id", "DisplayName");
                        return PartialView("Modify", newcontent);
                    }

                case "Details":
                    {

                        var content = ContentManagerComponent.Instance.ContentFacade.Get(contentId);
                        return PartialView("DetailInfo", content.GetContent(SessionParameters.Culture));

                    }
            }

            return null;

        }

        public ActionResult ChangeCulture(int contentId, string langid)
        {
            if (string.IsNullOrEmpty(langid)) return null;
            var contenet = new ContentContent() { LanguageId = langid };
            if (contentId != 0)
            {
                var contentContent = ContentManagerComponent.Instance.ContentContentFacade.Get(contentId, langid);
                if (contentContent != null) contenet = contentContent;
            }
            ViewBag.Lanuages = new SelectList(CommonComponent.Instance.LanguageFacade.Where(x=>x.Enabled), "Id", "DisplayName");
            return PartialView("Modify", contenet);
        }


    }
}
