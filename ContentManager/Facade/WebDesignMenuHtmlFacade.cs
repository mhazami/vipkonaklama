using System;
using System.Data;
using Radyn.ContentManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.ContentManager.BO;

namespace Radyn.ContentManager.Facade
{
    internal sealed class WebDesignMenuHtmlFacade : ContentManagerBaseFacade<ContentManager.DataStructure.WebDesignMenuHtml>, ContentManager.Facade.Interface.IWebDesignMenuHtmlFacade
    {
        internal WebDesignMenuHtmlFacade()
        {
        }

        internal WebDesignMenuHtmlFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

    

        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                var CongressMenuHtmlBO = new WebDesignMenuHtmlBO();
                var obj = CongressMenuHtmlBO.Get(this.ConnectionHandler, keys);
                if (!CongressMenuHtmlBO.Delete(this.ConnectionHandler, keys))
                    throw new Exception("خطایی در حذف Html  وجود دارد");
                if (
                    !new MenuHtmlBO()
                        .Delete(ConnectionHandler,obj.MenuHtmlId))
                    throw new Exception("خطایی در حذف Html وجود دارد");
                this.ConnectionHandler.CommitTransaction();
               return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

     

        public bool Insert(Guid WebSiteId, MenuHtml htmlDesgin)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                htmlDesgin.IsExternal = true;
                htmlDesgin.CurrentUICultureName = htmlDesgin.CurrentUICultureName;
                if (!new MenuHtmlBO().Insert(ConnectionHandler, htmlDesgin))
                    throw new Exception("خطایی در ذخیره Html وجود دارد");
                var CongressMenuHtml = new DataStructure.WebDesignMenuHtml { MenuHtmlId = htmlDesgin.Id, WebSiteId = WebSiteId };
                if (!new WebDesignMenuHtmlBO().Insert(this.ConnectionHandler, CongressMenuHtml))
                    throw new Exception("خطایی در ذخیره Html وجود دارد");
                this.ConnectionHandler.CommitTransaction();
               return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
               Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

      
    }
}
