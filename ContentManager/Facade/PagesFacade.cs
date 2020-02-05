using Radyn.ContentManager.BO;
using Radyn.ContentManager.DataStructure;
using Radyn.ContentManager.Facade.Interface;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using System;
using System.Data;

namespace Radyn.ContentManager.Facade
{
    public class PagesFacade : ContentManagerBaseFacade<Pages>, IPagesFacade
    {
        internal PagesFacade()
        {
        }

        internal PagesFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

        public bool Insert(Pages obj, HtmlDesgin htmlDesgin)
        {
            try
            {
                ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
              if (!new PagesBO().Insert(ConnectionHandler, obj, htmlDesgin))
                    throw new Exception("خطا در ذخیره سازی اطلاعات");
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

        public bool Update(Pages obj, HtmlDesgin htmlDesgin)
        {
            try
            {


                ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                
                if (!new PagesBO().Update(ConnectionHandler, obj, htmlDesgin))
                    throw new Exception("خطا در ذخیره سازی اطلاعات");
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

        public override bool Delete(Pages obj)
        {
            try
            {
                ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new PagesBO().Delete(ConnectionHandler, obj))
                {
                    throw new Exception("خطا در حذف سازی اطلاعات");
                }
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
