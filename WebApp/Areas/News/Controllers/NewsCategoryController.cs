using System;
using System.Linq;
using System.Web.Mvc;
using Radyn.News;
using Radyn.News.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.News.Controllers
{
    public class NewsCategoryController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = NewsComponent.Instance.NewsCategoryFacade.GetAll();
            return View(list);
        }
       
        [RadynAuthorize]
        public ActionResult Details(Guid Id)
        {
            return View(NewsComponent.Instance.NewsCategoryFacade.Get(Id));
        }

        [RadynAuthorize]
        public ActionResult Create()
        {
            ViewBag.NewsCategory = new SelectList(NewsComponent.Instance.NewsCategoryFacade.GetAll(), "Id", "Title","");
            return View(new NewsCategory());
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Create(FormCollection collection)
        {
            var newsCategory = new NewsCategory();
            try
            {
                this.RadynTryUpdateModel(newsCategory);
                newsCategory.Id = Guid.NewGuid();
                newsCategory.CurrentUICultureName = collection["LanguageId"];
                if (NewsComponent.Instance.NewsCategoryFacade.Insert(newsCategory))
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
                ViewBag.NewsCategory = new SelectList(NewsComponent.Instance.NewsCategoryFacade.GetAll(), "Id", "Title");
                return View(newsCategory);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid Id)
        {
            ViewBag.NewsCategory = new SelectList(NewsComponent.Instance.NewsCategoryFacade.GetAll(), "Id", "Title","");
            return View(NewsComponent.Instance.NewsCategoryFacade.Get(Id));
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            var newsCategory = NewsComponent.Instance.NewsCategoryFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(newsCategory);
                newsCategory.CurrentUICultureName = collection["LanguageId"];
                if (NewsComponent.Instance.NewsCategoryFacade.Update(newsCategory))
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
                ViewBag.NewsCategory = new SelectList(NewsComponent.Instance.NewsCategoryFacade.GetAll(), "Id", "Title");
                return View(newsCategory);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Guid Id)
        {
            return View(NewsComponent.Instance.NewsCategoryFacade.Get(Id));
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Delete(Guid Id, FormCollection collection)
        {
            var newsCategory = NewsComponent.Instance.NewsCategoryFacade.Get(Id);
            try
            {
                if (NewsComponent.Instance.NewsCategoryFacade.Delete(Id))
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
                return View(newsCategory);
            }
        }
        public ActionResult CategoryList()
        {
            var list = NewsComponent.Instance.NewsCategoryFacade.GetAll().ToList();
            if (list.Count == 1)
            {
               return RedirectToAction("NewsList", "News", new { Area = "News", groupId = list[0].Id });
            }
            return View(list.ToList());
        }
    }
}
