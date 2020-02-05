using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Radyn.Article;
using Radyn.Article.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;
using Radyn.Utility;

namespace Radyn.WebApp.Areas.Articles.Controllers
{
    public class ArticlesController : WebDesignBaseController<Radyn.Article.DataStructure.Article>
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = ArticleComponent.Instance.ArticleFacade.OrderBy(x=>x.Order);
            return View(list);
        }
        public ActionResult GetDetails(Guid Id)
        {
            var obj = ArticleComponent.Instance.ArticleFacade.Get(Id);
            return PartialView("PVDetails", obj);
        }
        public ActionResult GetModify(Guid? Id, PageMode status,string culture)
        {
            Radyn.Article.DataStructure.Article article = null;
            if (string.IsNullOrEmpty(culture))
                culture = SessionParameters.Culture;
            switch (status)
            {

                case PageMode.Edit:
                    article = ArticleComponent.Instance.ArticleFacade.GetLanuageContent(culture,Id);
                    break;
                case PageMode.Create:
                    article = new Radyn.Article.DataStructure.Article
                    {
                        Id = Guid.NewGuid(),
                        PublishDate = DateTime.Now.ShamsiDate(),
                        Enable = true
                    };
                    break;

            }
            this.PrepareViewBags(article, status,culture);
            ViewBag.ArticleCategory = new SelectList(ArticleComponent.Instance.ArticleCategoryFacade.OrderBy(x => x.Order), "Id", "Title");
            return PartialView("PVModify", article);
        }

        [RadynAuthorize]
        public ActionResult Details(Guid Id)
        {

            ViewBag.Id = Id;
            return View();
        }

        [RadynAuthorize]
        public ActionResult Create()
        {

            return View();
        }
        
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(FormCollection collection)
        {
            var article = new Radyn.Article.DataStructure.Article();
            try
            {
              this.RadynTryUpdateModel(article, collection);
                HttpPostedFileBase file = null;
                if (RadynSession["Image"] != null)
                {
                    file = (HttpPostedFileBase)RadynSession["Image"];
                    
                }
                List<ArticleFileAttachment> MultiFile = null;
                if (RadynSession["ArticleFileAttachmentDataSource"] != null)
                {
                    MultiFile = (List<ArticleFileAttachment>)RadynSession["ArticleFileAttachmentDataSource"];
                  
                }
                article.ArticleFileAttachments = MultiFile;
                if (ArticleComponent.Instance.ArticleFacade.Insert(article, file))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    RadynSession.Remove("ArticleFileAttachmentDataSource");
                    RadynSession.Remove("Image");
                    return Content("true");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Content("false");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInInsert + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Content("false");
            }
        }








        [RadynAuthorize]
        public ActionResult Edit(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }

      
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(FormCollection collection)
        {
           
            try
            {
                var key = this.GetModelKey(collection);
                var article = ArticleComponent.Instance.ArticleFacade.Get(key);
                HttpPostedFileBase file = null;
                this.RadynTryUpdateModel(article, collection);
                if (RadynSession["Image"] != null)
                {
                    file = (HttpPostedFileBase)RadynSession["Image"];
                    
                }
                if (ArticleComponent.Instance.ArticleFacade.Update(article, file))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle,
                                 messageIcon: MessageIcon.Succeed);
                    RadynSession.Remove("Image");
                    return Content("true");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Content("false");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInEdit + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Content("false");
            }
        }


        [RadynAuthorize]
        public ActionResult Delete(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        [RadynAuthorize]
        [HttpPost]
        public ActionResult Delete(Guid Id, FormCollection collection)
        {
            var article = ArticleComponent.Instance.ArticleFacade.Get(Id);

            try
            {
                if (ArticleComponent.Instance.ArticleFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/Articles/Articles/Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Articles/Articles/Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInDelete + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                ViewBag.Id = Id;
                return View(article);
            }
        }




    }
}
