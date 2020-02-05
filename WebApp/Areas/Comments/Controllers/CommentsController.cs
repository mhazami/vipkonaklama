using Radyn.Comments;
using Radyn.Comments.DataStructure;
using Radyn.WebApp.AppCode.Base;
using System;
using System.Web.Mvc;

namespace Radyn.WebApp.Areas.Comments.Controllers
{
    public class CommentsController : WebDesignBaseController
    {
        // GET: Comments/Comments


        public ActionResult Details(Guid id)
        {
            Comment comment = new Comment();
            return PartialView("PVDetails", comment);
        }


        public ActionResult Modify()
        {
            Comment comment = new Comment();
            return PartialView("PVModify", comment);
        }


        public ActionResult GetReplies(Guid commentId)
        {
            var comments =
                CommentsComponent.Instance.CommentFacade.OrderByDescending(x => x.SendDate + " " + x.SendTime,
                    x => x.ParentCommentId == commentId && x.Approved);
            return PartialView("PVReplye", comments);
        }
    }
}