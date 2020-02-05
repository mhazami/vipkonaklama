using System;
using System.Web.Mvc;
using Radyn.Article;
using Radyn.Article.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Articles.Controllers
{
    public class ArticleResourceController : WebDesignBaseController<ArticleResource>
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = ArticleComponent.Instance.ArticleResourceFacade.OrderBy(x=>x.Title);
            if (list.Count == 0) return this.Redirect("~/Articles/ArticleResource/Create");
            return View(list);
        }
        public ActionResult GetDetail(int Id)
        {

            return PartialView("PVDetails", ArticleComponent.Instance.ArticleResourceFacade.Get(Id));
        }
        public ActionResult GetModify(int? Id, Guid articleId)
        {
           
            return PartialView("PVModify", Id.HasValue ? ArticleComponent.Instance.ArticleResourceFacade.Get(Id) : new ArticleResource());
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

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var province = new ArticleResource();
            try
            {
                this.RadynTryUpdateModel(province);
                if (ArticleComponent.Instance.ArticleResourceFacade.Insert(province))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return (!string.IsNullOrEmpty(Request.QueryString["AddNew"])) ? this.Redirect("~/Articles/ArticleResource/Create") : this.Redirect("~/Articles/ArticleResource/Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Articles/ArticleResource/Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(province);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Int32 Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Int32 Id, FormCollection collection)
        {
            var province = ArticleComponent.Instance.ArticleResourceFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(province);
                if (ArticleComponent.Instance.ArticleResourceFacade.Update(province))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/Articles/ArticleResource/Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Articles/ArticleResource/Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Id = Id;
                return View(province);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Int32 Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Int32 Id, FormCollection collection)
        {
            var province = ArticleComponent.Instance.ArticleResourceFacade.Get(Id);
            try
            {
                if (ArticleComponent.Instance.ArticleResourceFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/Articles/ArticleResource/Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Articles/ArticleResource/Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Id = Id;
                return View(province);
            }
        }
    }
}
