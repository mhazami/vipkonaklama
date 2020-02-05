using Radyn.News;
using Radyn.News.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Radyn.WebApp.Areas.News.Controllers
{
    public class NewsCommentController : WebDesignBaseController
    {
        // GET: News/NewsComment
        public ActionResult Index()
        {
            var list = NewsComponent.Instance.NewsCommentFacade.GetAll();
            return View(list);
        }


        public ActionResult Create(int neswId)
        {
            NewsComment newsComment = new NewsComment() { NewsId = neswId };

            return PartialView("PVModify", newsComment);

        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            NewsComment newsComment = new NewsComment();

            try
            {
                RadynTryUpdateModel(newsComment.Comment, collection);
                RadynTryUpdateModel(newsComment, collection);
                if (!NewsComponent.Instance.NewsCommentFacade.Insert(newsComment))
                {
                    ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                    return View(newsComment);
                }
                ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                return View(newsComment);
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
                return View(newsComment);
            }
        }


        public ActionResult Details(Guid commentId)
        {
            var newComment = new NewsComment() { CommentId = commentId };
            return View(newComment);
        }

        public ActionResult ShowNewsComment(int newsId)
        {
            var result = NewsComponent.Instance.NewsCommentFacade.Select(x => x.Comment,
                    x => x.NewsId == newsId && x.Comment.Approved && x.Comment.ParentCommentId == null)
                .OrderByDescending(x => x.SendDate + " " + x.SendTime);
            ViewBag.NewsId = newsId;
            return PartialView("PVShowComments", result);
        }




    }
}