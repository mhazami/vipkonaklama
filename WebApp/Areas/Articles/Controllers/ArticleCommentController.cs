using Radyn.Article;
using Radyn.Article.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Radyn.WebApp.Areas.Articles.Controllers
{
    public class ArticleCommentController : WebDesignBaseController
    {
        // GET: Articles/ArticleComment
        public ActionResult Index()
        {
            var list = ArticleComponent.Instance.ArticleCommentFacade.GetAll();
            return View(list);
        }


        public ActionResult Create(Guid articleId)
        {
            ArticleComment ArticleComment = new ArticleComment() { ArticleId = articleId };

            return PartialView("PVModify", ArticleComment);

        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            ArticleComment ArticleComment = new ArticleComment();

            try
            {
                RadynTryUpdateModel(ArticleComment.Comment, collection);
                RadynTryUpdateModel(ArticleComment, collection);
                if (!ArticleComponent.Instance.ArticleCommentFacade.Insert(ArticleComment))
                {
                    ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                    return Content("false");
                }
                ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                return Content("true");
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
                return Content("false");
            }
        }


        public ActionResult Details(Guid commentId)
        {
            var newComment = new ArticleComment() { CommentId = commentId };
            return View(newComment);
        }

        public ActionResult ShowArticleComment(Guid articleId)
        {
            var result = ArticleComponent.Instance.ArticleCommentFacade.Select(x => x.Comment,
                    x => x.ArticleId == articleId && x.Comment.Approved && x.Comment.ParentCommentId == null)
                .OrderByDescending(x => x.SendDate + " " + x.SendTime);
            ViewBag.ArticleId = articleId;
            return PartialView("PVShowComments", result);
        }
    }
}