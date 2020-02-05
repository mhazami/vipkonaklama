using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using Radyn.Article.BO;
using Radyn.Article.Facade.Interface;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Article.Facade
{
    internal sealed class ArticleFacade : ArticleBaseFacade<Article.DataStructure.Article>, IArticleFacade
    {
        internal ArticleFacade()
        {
        }

        internal ArticleFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

       

       

        public bool Insert(DataStructure.Article obj, HttpPostedFileBase file)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new ArticleBO().Insert(this.ConnectionHandler,this.FileManagerConnection,obj, file))
                    throw new Exception("خطایی در ذخیره  محتوای مقاله وجود دارد");
                this.FileManagerConnection.CommitTransaction();
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.FileManagerConnection.RollBack();
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.FileManagerConnection.RollBack();
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

      
        public bool Update(DataStructure.Article obj, HttpPostedFileBase file)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new ArticleBO().Update(this.ConnectionHandler, this.FileManagerConnection, obj, file))
                    throw new Exception("خطایی در ویرایش  محتوای مقاله وجود دارد");
               this.FileManagerConnection.CommitTransaction();
               this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.FileManagerConnection.RollBack();
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.FileManagerConnection.RollBack();
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public IEnumerable<DataStructure.Article> GetByCategoryId(short categoryId, int? topCount)
        {
            try
            {
                var list = new ArticleBO().GetByCategoryId(this.ConnectionHandler, categoryId, topCount);
                return list;
            }
            catch (KnownException ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                var articleBO = new ArticleBO();
                var obj = articleBO.Get(this.ConnectionHandler, keys);
                var articleResourcesBO = new ArticleResourcesBO();
                var resoures = articleResourcesBO.Where(ConnectionHandler,
                    resources => resources.ArticleId == obj.Id);
                foreach (var articleResourcese in resoures)
                {
                    if (
                        !articleResourcesBO.Delete(this.ConnectionHandler, articleResourcese.ArticleId,
                            articleResourcese.ArticleResourceId))
                        throw new Exception("خطایی در حذف منابع مقاله وجود دارد");

                }
              
                var articleFileAttachmentBO = new ArticleFileAttachmentBO();
                var articleFileAttachments = articleFileAttachmentBO.Where(ConnectionHandler,
                    resources => resources.ArticleId == obj.Id);
                foreach (var articleFileAttachment in articleFileAttachments)
                {
                    if (
                        !articleFileAttachmentBO.Delete(this.ConnectionHandler, articleFileAttachment.ArticleId,
                            articleFileAttachment.FileId))
                        throw new Exception("خطایی در حذف فایل پیوست مقاله وجود دارد");

                }
                var fileId = obj.ThumbnailId;
                if (!articleBO.Delete(this.ConnectionHandler, keys))
                    throw new Exception("خطایی در حذف مقاله وجود دارد");
                if (fileId.HasValue)
                {
                    if (
                        !FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection)
                            .Delete(fileId))
                        throw new Exception("خطایی در حذف فایل پیوست مقاله وجود دارد");
                }


                var comments = new ArticleCommentBO().Where(this.ConnectionHandler, x => x.ArticleId == obj.Id);
                foreach (var articleComment in comments)
                {
                    new ArticleCommentBO().Delete(this.ConnectionHandler, articleComment);
                }



                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }
    }
}
