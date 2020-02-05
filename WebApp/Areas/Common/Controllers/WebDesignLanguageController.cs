using System;
using System.Web;
using System.Web.Mvc;
using Radyn.Common;
using Radyn.Common.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Common.Controllers
{
    public class WebDesignLanguageController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = CommonComponent.Instance.WebDesignLanguageFacade.GetByWebSiteId(this.WebSite.Id);
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(string Id)
        {
            ViewBag.Id = Id;
            return View();

        }

        [RadynAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Create(FormCollection collection)
        {
            var language = new Language();

            try
            {
                this.RadynTryUpdateModel(language, collection);

                HttpPostedFileBase image = null;
                if (RadynSession["Image"] != null)
                {
                    image = (HttpPostedFileBase)RadynSession["Image"];
                    RadynSession.Remove("Image");
                }

                if (CommonComponent.Instance.WebDesignLanguageFacade.Insert(this.WebSite.Id, language, image))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInInsert + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(language);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(string Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Edit(string Id, FormCollection collection)
        {
            var language = CommonComponent.Instance.LanguageFacade.Get(Id);

            try
            {

                this.RadynTryUpdateModel(language, collection);
                HttpPostedFileBase image = null;
                if (RadynSession["Image"] != null)
                {
                    image = (HttpPostedFileBase)RadynSession["Image"];
                    RadynSession.Remove("Image");
                }

                if (CommonComponent.Instance.LanguageFacade.Update(language, image))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInEdit + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                ViewBag.Id = Id;
                return View(language);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(string Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Delete(string Id, FormCollection collection)
        {
            try
            {
                if (CommonComponent.Instance.WebDesignLanguageFacade.Delete(this.WebSite.Id, Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInDelete + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                ViewBag.Id = Id;
                return View();
            }
        }



        public ActionResult CultureBar()
        {
            var list = CommonComponent.Instance.WebDesignLanguageFacade.GetValidList(this.WebSite.Id);
            return PartialView("LanguageBar", list);
        }


    }
}
