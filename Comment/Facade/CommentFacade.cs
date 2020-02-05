using Radyn.Comments.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using System;
using System.Data;
using Radyn.Comments.BO;

namespace Radyn.Comments.Facade
{
    internal sealed class CommentFacade : CommentsBaseFacade<Comment>, ICommentFacade
    {
        internal CommentFacade() { }

        internal CommentFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }


        public override bool Delete(params object[] keys)
        {
            try
            {
                ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                var comment = Get(keys);
                new CommentBO().Delete(this.ConnectionHandler, comment);
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


        public override bool Delete(Comment obj)
        {
            try
            {
                ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                new CommentBO().Delete(this.ConnectionHandler, obj);
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
    }
}

