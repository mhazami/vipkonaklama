using System;
using System.Data;
using Radyn.Article.BO;
using Radyn.Article.DataStructure;
using Radyn.Article.Facade.Interface;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Article.Facade
{
    internal sealed class ArticleFileAttachmentFacade : ArticleBaseFacade<ArticleFileAttachment>,
        IArticleFileAttachmentFacade
    {
        internal ArticleFileAttachmentFacade()
        {
        }

        internal ArticleFileAttachmentFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

       
        public override bool Insert(ArticleFileAttachment ArticleFileAttachment)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
               var articleFileAttachmentBO = new ArticleFileAttachmentBO();
                articleFileAttachmentBO.Insert(this.ConnectionHandler, this.FileManagerConnection, ArticleFileAttachment);
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
        public override bool Update(ArticleFileAttachment ArticleFileAttachment)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                var articleFileAttachmentBO = new ArticleFileAttachmentBO();
                articleFileAttachmentBO.Update(this.ConnectionHandler, this.FileManagerConnection, ArticleFileAttachment);
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
