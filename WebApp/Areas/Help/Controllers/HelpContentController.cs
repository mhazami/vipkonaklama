using System;
using System.Linq;
using System.Web.Mvc;
using Radyn.Common;
using Radyn.Common.DataStructure;
using Radyn.Help;
using Radyn.Help.DataStructure;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;

namespace Radyn.WebApp.Areas.Help.Controllers
{
    public class HelpContentController : WebDesignBaseController
    {
        public ActionResult Index(Guid helpId)
        {
            var list = HelpComponent.Instance.HelpContentFacade.Where(content => content.HelpId==helpId);
            return View(list);
        }

        public ActionResult Details(Guid helpId, string languageId)
        {
            var helpContent = HelpComponent.Instance.HelpContentFacade.Get(helpId, languageId);
            return View(helpContent);
        }

        public ActionResult Create(Guid helpId)
        {
            var helpContents = HelpComponent.Instance.HelpContentFacade.Where(content => content.HelpId == helpId);

            var predicateBuilder = new PredicateBuilder<Language>();

            foreach (var item in helpContents)
            {
                var item1 = item;
                predicateBuilder.And(x => !x.Id.Equals(item1.LanguageId));
            }
            var languagelist = predicateBuilder.Where(CommonComponent.Instance.LanguageFacade.GetAll());
            if (!languagelist.Any())
            {
                ShowMessage(Resources.Help.AllLanguageContentIsDefined, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index", new { helpId = helpId });
            }
            ViewBag.Language = new SelectList(languagelist, "Id", "DisplayName");
            ViewBag.HelpTitle = HelpComponent.Instance.HelpFacade.Get(helpId).DefaultTitle;
            return View(new HelpContent());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(FormCollection collection, Guid helpId)
        {
            var helpContent = new HelpContent();
          
            try
            {
                this.RadynTryUpdateModel(helpContent, collection);
                helpContent.CreateDate = DateTime.Now;
                helpContent.LastUpdate = DateTime.Now;
                helpContent.HelpId = helpId;
                if (HelpComponent.Instance.HelpContentFacade.Insert(helpContent))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index", new { helpId = helpId });
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index", new { helpId = helpId });
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(helpContent);
            }
        }

        public ActionResult Edit(Guid helpId, string languageId)
        {
            ViewBag.HelpTitle = HelpComponent.Instance.HelpFacade.Get(helpId).DefaultTitle;
            ViewBag.Language = CommonComponent.Instance.LanguageFacade.Get(languageId).DisplayName;
            var helpContent = HelpComponent.Instance.HelpContentFacade.Get(helpId, languageId);
            return View(helpContent);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Guid helpId, string languageId, FormCollection collection)
        {
            var helpContent = HelpComponent.Instance.HelpContentFacade.Get(helpId, languageId);
            
            try
            {
                this.RadynTryUpdateModel(helpContent, collection);
                if (HelpComponent.Instance.HelpContentFacade.Update(helpContent))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index", new { helpId = helpId });
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index", new { helpId = helpId });
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(helpContent);
            }
        }

        public ActionResult Delete(Guid helpId, string languageId)
        {
            return View(HelpComponent.Instance.HelpContentFacade.Get(helpId, languageId));
        }

        [HttpPost]
        public ActionResult Delete(Guid helpId, string languageId, FormCollection collection)
        {
            var helpContent = HelpComponent.Instance.HelpContentFacade.Get(helpId, languageId);
            
            try
            {   // UNDONE: Delete method does not work right 
                if (HelpComponent.Instance.HelpContentFacade.Delete(helpId, languageId))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index", new { helpId = helpId });
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index", new { helpId = helpId });
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(helpContent);
            }
        }
    }
}
