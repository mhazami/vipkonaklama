using System;
using System.Collections.Generic;
using System.Data;
using Radyn.Article.BO;
using Radyn.Article.DataStructure;
using Radyn.Article.Facade.Interface;
using Radyn.Comments.DataStructure;
using Radyn.Framework;

namespace Radyn.Article.Facade
{
    public class ArticleCommentFacade : ArticleBaseFacade<DataStructure.ArticleComment>, IArticleCommentFacade
    {
        public override bool Delete(params object[] keys)
        {
            try
            {
                ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                var comment = Get(keys);
                new ArticleCommentBO().Delete(this.ConnectionHandler, comment);
                ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }


        public override bool Delete(ArticleComment obj)
        {
            try
            {
                ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                new ArticleCommentBO().Delete(this.ConnectionHandler, obj);
                ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public List<Comment> GetWithReply(Guid articleId)
        {
            return new ArticleCommentBO().GetWithReply(this.ConnectionHandler, articleId);
        }
    }
}
