using System;
using System.Web.Mvc;
using Radyn.News;
using Radyn.News.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.News.Controllers
{
    public class PublishTypeController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = NewsComponent.Instance.PublishTypeFacade.GetAll();
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(Int32 Id)
        {
            return View(NewsComponent.Instance.PublishTypeFacade.Get(Id));
        }

        [RadynAuthorize]
        public ActionResult Create()
        {
            return View(new PublishType());
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Create(FormCollection collection)
        {
            var publishType = new PublishType();
            try
            {
                this.RadynTryUpdateModel(publishType, collection);
                publishType.CurrentUICultureName = collection["LanguageId"];
                if (NewsComponent.Instance.PublishTypeFacade.Insert(publishType))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(publishType);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Int32 Id)
        {
            return View(NewsComponent.Instance.PublishTypeFacade.Get(Id));
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Edit(Int32 Id, FormCollection collection)
        {
            var publishType = NewsComponent.Instance.PublishTypeFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(publishType, collection);
                publishType.CurrentUICultureName = collection["LanguageId"];
                if (NewsComponent.Instance.PublishTypeFacade.Update(publishType))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(publishType);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Int32 Id)
        {
            return View(NewsComponent.Instance.PublishTypeFacade.Get(Id));
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Delete(Int32 Id, FormCollection collection)
        {
            var publishType = NewsComponent.Instance.PublishTypeFacade.Get(Id);
            try
            {
                if (NewsComponent.Instance.PublishTypeFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(publishType);
            }
        }
    }
}
