using System;
using System.Web.Mvc;
using Radyn.News;
using Radyn.News.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.News.Controllers
{
    public class NewsContentTypeController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = NewsComponent.Instance.NewsContentTypeFacade.GetAll();
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(Int32 Id)
        {
            return View(NewsComponent.Instance.NewsContentTypeFacade.Get(Id));
        }

        [RadynAuthorize]
        public ActionResult Create()
        {
            return View(new NewsContentType());
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Create(FormCollection collection)
        {
            var newsContentType = new NewsContentType();
            try
            {
                this.RadynTryUpdateModel(newsContentType, collection);
              
                newsContentType.CurrentUICultureName = collection["LanguageId"];
                if (NewsComponent.Instance.NewsContentTypeFacade.Insert(newsContentType))
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
                return View(newsContentType);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Int32 Id)
        {
            return View(NewsComponent.Instance.NewsContentTypeFacade.Get(Id));
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Edit(Int32 Id, FormCollection collection)
        {
            var newsContentType = NewsComponent.Instance.NewsContentTypeFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(newsContentType, collection);
                newsContentType.CurrentUICultureName = collection["LanguageId"];

                if (NewsComponent.Instance.NewsContentTypeFacade.Update(newsContentType))
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
                return View(newsContentType);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Int32 Id)
        {
            return View(NewsComponent.Instance.NewsContentTypeFacade.Get(Id));
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Delete(Int32 Id, FormCollection collection)
        {
            var newsContentType = NewsComponent.Instance.NewsContentTypeFacade.Get(Id);
            try
            {
                if (NewsComponent.Instance.NewsContentTypeFacade.Delete(Id))
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
                return View(newsContentType);
            }
        }
    }
}
