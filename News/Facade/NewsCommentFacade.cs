using Radyn.Framework;
using Radyn.News.DataStructure;
using Radyn.News.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using Radyn.Comments.BO;
using Radyn.Comments.DataStructure;
using Radyn.News.BO;

namespace Radyn.News.Facade
{
    public class NewsCommentFacade : NewsBaseFacade<DataStructure.NewsComment>, INewsCommentFacade
    {
        public override bool Insert(NewsComment obj)
        {
            try
            {
                ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                new CommentBO().Insert(this.ConnectionHandler, obj.Comment);

                new NewsCommentBO().Insert(this.ConnectionHandler, obj);
                ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                ConnectionHandler.RollBack();
                FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                ConnectionHandler.RollBack();
                FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public List<Comment> GetWithReply(int newsId)
        {
            return new NewsCommentBO().GetWithReply(this.ConnectionHandler, newsId);
        }
    }
}
