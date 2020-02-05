using System;
using System.Web.Mvc;
using Radyn.Common;
using Radyn.FAQ;
using Radyn.FAQ.DataStructure;
using Radyn.FAQ.Tools;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.FAQ.Controllers
{
    public class FAQContentController : WebDesignBaseController
    {
        public ActionResult Modify(Guid faqId, string state)
        {
            switch (state)
            {
                case "Modify":
                    {
                        var newcontent = new FAQContent() { LanguageId = SessionParameters.Culture };
                        if (faqId != Guid.Empty)
                        {
                            var content = FAQComponent.Instance.FAQFacade.Get(faqId);
                            if (content != null)
                                newcontent = content.Content(SessionParameters.Culture);
                        }
                       
                        ViewBag.Lanuages = new SelectList(CommonComponent.Instance.LanguageFacade.Where(x=>x.Enabled), "Id", "DisplayName");
                        return PartialView("PVModify", newcontent);
                    }

                case "Details":
                    {
                        var content = FAQComponent.Instance.FAQFacade.Get(faqId);
                        return PartialView("PVDetailInfo", content.Content(SessionParameters.Culture));
                    }
            }
            return null;

        }

        public ActionResult ChangeCulture(Guid faqId, string langid)
        {
            if (string.IsNullOrEmpty(langid)) return null;
            var contenet = new FAQContent() { LanguageId = langid };
            if (faqId != Guid.Empty)
            {
                var itemContent = FAQComponent.Instance.FaqContentFacade.Get(faqId, langid);
                if (itemContent != null) contenet = itemContent;
            }
            ViewBag.Lanuages = new SelectList(CommonComponent.Instance.LanguageFacade.GetAll(), "Id", "DisplayName");
            return PartialView("PVModify", contenet);
        }


    }
}
