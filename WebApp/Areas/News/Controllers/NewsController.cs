using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Radyn.News;
using Radyn.News.DataStructure;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.News.Controllers
{
    public class NewsController : WebDesignBaseController
    {

        public ActionResult GetDetails(Int32 Id)
        {
            var obj = NewsComponent.Instance.NewsFacade.Get(Id);
            return PartialView("PVDetails", obj);
        }
        public ActionResult GetModify(Int32? Id)
        {
            var newsCategoryFacade = NewsComponent.Instance.NewsCategoryFacade;
            var newsCategories = newsCategoryFacade.GetAll();
            if (newsCategories.Count == 0)
            {
                var newsCategory = new NewsCategory { Title = "گروه پیش فرض", Enabled = true, CurrentUICultureName = SessionParameters.Culture };
                if (!newsCategoryFacade.Insert(newsCategory))
                {
                    ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                }
            }
            ViewBag.NewsCategory = new SelectList(newsCategoryFacade.GetAll(), "Id", "Title");
            var news = Id.HasValue ? NewsComponent.Instance.NewsFacade.Get(Id) : new Radyn.News.DataStructure.News
            {
                PublishTime = DateTime.Now.ToShortTimeString(),
                PublishDate = DateTime.Now.ShamsiDate(),
                Enabled = true
            };

            return PartialView("PVModify", news);
        }

        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = NewsComponent.Instance.NewsFacade.Where(news => news.IsExternal == false);
            return View(list);
        }
        [RadynAuthorize]
        public ActionResult Details(Int32 Id)
        {

            ViewBag.Id = Id;
            return View();
        }

        [RadynAuthorize]
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
                var file = RadynSession["Image"];
                RadynSession.Remove("Image");
                if (SessionParameters.User != null) news.CreatorId = SessionParameters.User.Id;
                if (NewsComponent.Instance.NewsFacade.Insert(news, newsContent, newsproperty, (HttpPostedFileBase)file))
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
                return View(news);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(int id)
        {

            ViewBag.Id = id;
            return View();
        }

        [RadynAuthorize]
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection collection)
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
                var file = RadynSession["Image"];
                RadynSession.Remove("Image");
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
                ShowExceptionMessage(exception);
                ViewBag.Id = id;
                return View(news);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        [RadynAuthorize]
        public ActionResult Delete(int id, FormCollection collection)
        {

            try
            {
                if (NewsComponent.Instance.NewsFacade.Delete(id))
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
                ViewBag.Id = id;
                return View();
            }
        }
        
        public ActionResult NewListByCategory(Guid? categoryId, int topCount = 0)
        {
            IEnumerable<Radyn.News.DataStructure.News> list;
            if (categoryId != null)
            {
                var newsCategory = NewsComponent.Instance.NewsCategoryFacade.Get(categoryId);
                if (newsCategory != null)
                    ViewBag.Category = newsCategory;
                list = NewsComponent.Instance.NewsFacade.GetByCategory((Guid)categoryId, topCount);
            }
            else list = NewsComponent.Instance.NewsFacade.GetTopNews(topCount);
            return PartialView("PartialNewListByCategory", list);

        }
        public ActionResult GetNews()
        {
            var list = NewsComponent.Instance.NewsFacade.GetTopNews();
            return PartialView("PartialViewNews", list);
        }

        public ActionResult NewsView(int Id)
        {

            return View(NewsComponent.Instance.NewsFacade.Get(Id));
        }
        public ActionResult SearchNews()
        {
            return PartialView("PartialViewSearch");
        }
        public ActionResult Search()
        {
            var list = NewsComponent.Instance.NewsFacade.Search(Request.QueryString["q"]).ToList();
            return View(list);
        }
        public ActionResult NewsList(Guid? groupId)
        {
            if (groupId.HasValue)
            {
                var category = NewsComponent.Instance.NewsCategoryFacade.Get(groupId);
                ViewBag.Category = category;
            }
            var predicateBuilder = new PredicateBuilder<Radyn.News.DataStructure.News>();
            if (groupId.HasValue)
                predicateBuilder.And(x => x.NewsCategoryId == groupId);
            predicateBuilder.And(x => x.Enabled);

            var news = NewsComponent.Instance.NewsFacade.Where(predicateBuilder.GetExpression());
            if (news.Count == 0)
            {
                ShowMessage(Resources.News.This_Category_Not_have_News);
                return null;
            }

            return View(news.ToList());
        }


    }
}
