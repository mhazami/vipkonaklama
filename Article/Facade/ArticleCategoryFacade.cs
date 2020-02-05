using System;
using System.Data;
using System.Web;
using Radyn.Article.BO;
using Radyn.Article.DataStructure;
using Radyn.Article.Facade.Interface;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Article.Facade
{
    internal sealed class ArticleCategoryFacade : ArticleBaseFacade<ArticleCategory>, IArticleCategoryFacade
    {
        internal ArticleCategoryFacade()
        {
        }

        internal ArticleCategoryFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

        public bool Insert(DataStructure.ArticleCategory obj, HttpPostedFileBase file)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new ArticleCategoryBO().Insert(this.ConnectionHandler, this.FileManagerConnection, obj, file))
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


        public bool Update(DataStructure.ArticleCategory obj, HttpPostedFileBase file)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new ArticleCategoryBO().Update(this.ConnectionHandler, this.FileManagerConnection, obj, file))
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

      
        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                var articleBO = new ArticleCategoryBO();
                var obj = articleBO.Get(this.ConnectionHandler, keys);
               

                var fileId = obj.FileId;
                if (!articleBO.Delete(this.ConnectionHandler, keys))
                    throw new Exception("خطایی در حذف گروه مقاله وجود دارد");
                if (fileId.HasValue)
                {
                    if (
                        !FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection)
                            .Delete(fileId))
                        throw new Exception("خطایی در حذف فایل پیوست گروه مقاله وجود دارد");
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
