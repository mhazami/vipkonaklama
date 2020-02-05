using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Radyn.Article;
using Radyn.Article.DataStructure;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.Areas.FileManager.Extention;


namespace Radyn.WebApp.Areas.Articles.Controllers
{
    public class ArticleAttachmentController : WebDesignBaseController<ArticleFileAttachment>
    {

        public ActionResult Index(Guid articleId, ModifyBehavior behavior = ModifyBehavior.SelfModify, PageMode Status = PageMode.Edit)
        {
            try
            {
                ViewBag.articleId = articleId;
                switch (behavior)
                {
                    case ModifyBehavior.SelfModify:


                        break;
                    case ModifyBehavior.DependedModify:
                        this.DataSource = ArticleComponent.Instance.ArticleFileAttachmentFacade.Where(x => x.ArticleId == articleId, true);
                        break;

                }
                this.MB = behavior;
                this.ControllerStatus = Status;
                return PartialView("PVIndex");
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
                return null;
            }
        }

        public ActionResult GetIndex(Guid articleId)
        {
            try
            {
               
                List<ArticleFileAttachment> properties = null;
                switch (MB)
                {

                    case ModifyBehavior.DependedModify:
                        properties = DataSource;
                       break;

                    case ModifyBehavior.SelfModify:
                        properties = ArticleComponent.Instance.ArticleFileAttachmentFacade.Where(x => x.ArticleId == articleId);
                        break;

                }

              
                return PartialView("PVGetIndex", properties);
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
                return null;
            }
        }
        public ActionResult GetArticleAttachmentModify(Guid FileId, Guid articleId, PageMode status)
        {
            try
            {
                ArticleFileAttachment ArticleFileAttachment = null;
                switch (status)
                {
                    case PageMode.Create:
                        ArticleFileAttachment = new ArticleFileAttachment() { ArticleId = articleId,FileId = FileId};
                        break;
                    case PageMode.Edit:
                        switch (MB)
                        {

                            case ModifyBehavior.DependedModify:
                                ArticleFileAttachment = this.DataSource.FirstOrDefault(c => c.ArticleId == articleId);
                                break;

                            case ModifyBehavior.SelfModify:
                                ArticleFileAttachment = ArticleComponent.Instance.ArticleFileAttachmentFacade.Get(articleId, FileId);
                                break;

                        }
                        break;
                }
                this.PrepareViewBags(ArticleFileAttachment, status);
                return PartialView("PVArticleAttachmentModify", ArticleFileAttachment);
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
                return null;
            }
        }
        [HttpPost]
        public ActionResult GetArticleAttachmentModify(FormCollection collection)
        {

            try
            {
                HttpPostedFileBase file = null;
                if (RadynSession["File"] != null)
                {
                    file = (HttpPostedFileBase)RadynSession["File"];
                   
                }
                ArticleFileAttachment ArticleFileAttachment;
                PageMode pageMode = base.GetPageMode(collection);
                var id = base.GetModelKey(collection);
                var ArticleId = collection["ArticleId"].ToGuid();
                var fileId = collection["ModelFileId"].ToGuid();
                switch (pageMode)
                {
                    case PageMode.Edit:
                        switch (MB)
                        {
                            case ModifyBehavior.DependedModify:
                                ArticleFileAttachment = this.DataSource.FirstOrDefault(c => c.ArticleId == ArticleId && c.FileId == fileId);
                                if (ArticleFileAttachment == null) return Content("false");
                                this.DataSource.Remove(ArticleFileAttachment);
                                ArticleFileAttachment = new ArticleFileAttachment();
                                this.RadynTryUpdateModel(ArticleFileAttachment, collection);
                                ArticleFileAttachment.File = file.PraperFile(fileId);
                                ArticleFileAttachment.FileId = fileId;
                                this.DataSource.Add(ArticleFileAttachment);
                                RadynSession.Remove("File");
                                break;
                            case ModifyBehavior.SelfModify:
                                ArticleFileAttachment = ArticleComponent.Instance.ArticleFileAttachmentFacade.Get(id);
                                this.RadynTryUpdateModel(ArticleFileAttachment, collection);
                                ArticleFileAttachment.File = file.PraperFile(fileId);
                                ArticleFileAttachment.FileId = fileId;
                                if (!ArticleComponent.Instance.ArticleFileAttachmentFacade.Update(ArticleFileAttachment)) return Content("false");
                                RadynSession.Remove("File");
                                return Content("true");

                        }
                        break;
                    case PageMode.Create:
                        ArticleFileAttachment = new ArticleFileAttachment();
                        this.RadynTryUpdateModel(ArticleFileAttachment, collection);
                        ArticleFileAttachment.File = file.PraperFile(fileId);
                        ArticleFileAttachment.FileId = fileId;
                        switch (MB)
                        {
                            case ModifyBehavior.DependedModify:
                                this.DataSource.Add(ArticleFileAttachment);
                                RadynSession.Remove("File");
                                return Content("true");
                            case ModifyBehavior.SelfModify:
                                if (ArticleComponent.Instance.ArticleFileAttachmentFacade.Insert(ArticleFileAttachment))
                                {
                                    RadynSession.Remove("File");
                                    return Content("true");
                                }
                                return Content("false");

                        }
                        break;

                }
                return Content("false");


            }
            catch (Exception ex)

            {
                ShowExceptionMessage(ex);
                return Content("false");
            }




        }

        public ActionResult DeleteAttachment(Guid ArticleId, Guid fileId, ModifyBehavior behavior)
        {

            try
            {
                this.MB = behavior;
                switch (MB)
                {

                    case ModifyBehavior.DependedModify:
                        var find = this.DataSource.FirstOrDefault(c => c.ArticleId == ArticleId && c.FileId == fileId);
                        this.DataSource.Remove(find);

                        return Content("true");
                        break;

                    case ModifyBehavior.SelfModify:
                        if (ArticleComponent.Instance.ArticleFileAttachmentFacade.Delete(ArticleId, fileId))
                            return Content("true");
                        return Content("false");
                        break;

                }
                return Content("false");

            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInDelete + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Content("false");
            }
        }











    }
}
