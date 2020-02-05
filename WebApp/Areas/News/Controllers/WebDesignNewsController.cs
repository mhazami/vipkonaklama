using System;
using System.Web;
using System.Web.Mvc;
using Radyn.News;
using Radyn.News.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Filter;
using Radyn.WebApp.AppCode.Security;
using Radyn.WebApp.AppCode.Base;

namespace Radyn.WebApp.Areas.News.Controllers
{
    public class WebDesignNewsController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = NewsComponent.Instance.WebDesignNewsFacade.Select(x => x.WebSiteNews, x => x.WebId == this.WebSite.Id);
            return View(list);
        }
        [RadynAuthorize]
        public ActionResult Details(Int32 Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [RadynAuthorize]
        [SourceCodeFile("ImageBrowser Controller", "~/Controllers/ImageBrowserController.cs")]
        public ActionResult Create()
        {
            return View();
        }
        [RadynAuthorize]
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(FormCollection collection)
        {
            var news = new Radyn.News.DataStructure.News();
            try
            {
                var newsproperty = new NewsProperty();
                var newsContent = new NewsContent();
                RadynTryUpdateModel(news, collection);
                RadynTryUpdateModel(newsproperty, collection);
                RadynTryUpdateModel(newsContent, collection);
                news.SaveDate = DateTime.Now;
                var file = RadynSession["NewsImage"];
                RadynSession.Remove("NewsImage");
                if (SessionParameters.User != null) news.CreatorId = SessionParameters.User.Id;
                if (NewsComponent.Instance.WebDesignNewsFacade.Insert(this.WebSite.Id, news, newsContent, newsproperty, (HttpPostedFileBase)file))
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
                return View(news);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Int32 id)
        {
            ViewBag.Id = id;
            return View();
        }

        [RadynAuthorize]
        [HttpPost, ValidateInput(false)]
        [SourceCodeFile("ImageBrowser Controller", "~/Controllers/ImageBrowserController.cs")]
        public ActionResult Edit(Int32 id, FormCollection collection)
        {
            var news = NewsComponent.Instance.NewsFacade.Get(id);
            try
            {
                var newscontent = NewsComponent.Instance.NewsContentFacade.Get(id, collection["LanguageId"]) ??
                                  new NewsContent();
                var newsProperty = NewsComponent.Instance.NewsPropertyFacade.Get(id);
                this.RadynTryUpdateModel(news, collection);
                this.RadynTryUpdateModel(newsProperty, collection);
                this.RadynTryUpdateModel(newscontent, collection);
                var file = RadynSession["NewsImage"];
                RadynSession.Remove("NewsImage");
                if (SessionParameters.User != null) news.EditorId = SessionParameters.User.Id;
                if (NewsComponent.Instance.NewsFacade.Update(news, newscontent, newsProperty, (HttpPostedFileBase)file))
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
                ViewBag.Id = id;

                return View(news);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Int32 id)
        {

            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        [RadynAuthorize]
        public ActionResult Delete(Int32 id, FormCollection collection)
        {
            try
            {
                if (NewsComponent.Instance.WebDesignNewsFacade.Delete(this.WebSite.Id, id))
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
                ViewBag.Id = id;
                return View();
            }
        }

    }
}
