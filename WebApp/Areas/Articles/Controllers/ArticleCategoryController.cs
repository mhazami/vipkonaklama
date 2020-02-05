using System;
using System.Web;
using System.Web.Mvc;
using Radyn.Article;
using Radyn.Article.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Articles.Controllers
{
    public class ArticleCategoryController : WebDesignBaseController<ArticleCategory>
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = ArticleComponent.Instance.ArticleCategoryFacade.GetAll();
            if (list.Count == 0) return this.Redirect("~/Articles/ArticleCategory/Create");
            return View(list);
        }
        public ActionResult GetDetail(int Id)
        {
            
            return PartialView("PVDetails", ArticleComponent.Instance.ArticleCategoryFacade.Get(Id));
        }
        public ActionResult GetModify(int? Id)
        {
            ViewBag.ArticleCategory = new SelectList(ArticleComponent.Instance.ArticleCategoryFacade.OrderBy(x=>x.Order), "Id", "Title");
            return PartialView("PVModify", Id.HasValue ? ArticleComponent.Instance.ArticleCategoryFacade.Get(Id) : new ArticleCategory(){Enable = true});
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
            var province = new ArticleCategory();
            try
            {
                this.RadynTryUpdateModel(province);
                HttpPostedFileBase file = null;
                if (RadynSession["Image"] != null)
                {
                    file = (HttpPostedFileBase)RadynSession["Image"];

                }
                if (ArticleComponent.Instance.ArticleCategoryFacade.Insert(province, file))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    RadynSession.Remove("Image");
                    return (!string.IsNullOrEmpty(Request.QueryString["AddNew"])) ? this.Redirect("~/Articles/ArticleCategory/Create") : this.Redirect("~/Articles/ArticleCategory/Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Articles/ArticleCategory/Index");
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
            var province = ArticleComponent.Instance.ArticleCategoryFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(province);
                HttpPostedFileBase file = null;
                if (RadynSession["Image"] != null)
                {
                    file = (HttpPostedFileBase)RadynSession["Image"];

                }
                if (ArticleComponent.Instance.ArticleCategoryFacade.Update(province, file))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    RadynSession.Remove("Image");
                    return this.Redirect("~/Articles/ArticleCategory/Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Articles/ArticleCategory/Index");
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
            var province = ArticleComponent.Instance.ArticleCategoryFacade.Get(Id);
            try
            {
                if (ArticleComponent.Instance.ArticleCategoryFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/Articles/ArticleCategory/Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Articles/ArticleCategory/Index");
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
