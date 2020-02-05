using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Radyn.ContentManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.ContentManager.BO;
using Radyn.ContentManager.Facade.Interface;

namespace Radyn.ContentManager.Facade
{
    internal sealed class WebDesignHtmlFacade : ContentManagerBaseFacade<WebDesignHtml>, IWebDesignHtmlFacade
    {
        internal WebDesignHtmlFacade() { }

        internal WebDesignHtmlFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }


        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                var congressHtmlBo = new WebDesignHtmlBO();
                var obj = congressHtmlBo.Get(this.ConnectionHandler, keys);
                if (!congressHtmlBo.Delete(this.ConnectionHandler, keys))
                    throw new Exception("خطایی در حذف Html  وجود دارد");
                if (!new HtmlDesginBO().Delete(ConnectionHandler,obj.HtmlDesginId))
                    throw new Exception("خطایی در حذف Html وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException knownException)
            {
                this.ConnectionHandler.RollBack();
              throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                throw new KnownException(ex.Message, ex);
            }
        }




        public List<Partials> GetWebDesignContent(Guid webId, string culture)
        {
            try
            {
               
                var contents = new WebDesignContentBO().Select(this.ConnectionHandler, new Expression<Func<ContentManager.DataStructure.WebDesignContent, object>>[] { x => x.ContentId, x => x.WebSiteContent.Title, x => x.WebSiteContent.Enabled }, x => x.WebId == webId);
                var @select = contents.Select(i => (int)i.ContentId);
                return ContentManagerComponent.Instance.PartialsFacade.GetContentPartials(@select,culture);
                

               
            }

            catch (KnownException ex)
            {

                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {

                throw new KnownException(ex.Message, ex);
            }
        }

        public bool Insert(Guid websiteId, HtmlDesgin htmlDesgin)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
               htmlDesgin.IsExternal = true;
                var htmlDesginTransactinalFacade =
                    new HtmlDesginBO();
                htmlDesgin.CurrentUICultureName = htmlDesgin.CurrentUICultureName;
                if (!htmlDesginTransactinalFacade.Insert(ConnectionHandler,htmlDesgin))
                    throw new Exception("خطایی در ذخیره Html وجود دارد");
                var congressHtml = new WebDesignHtml { HtmlDesginId = htmlDesgin.Id, WebId = websiteId };
                if (!new WebDesignHtmlBO().Insert(this.ConnectionHandler, congressHtml))
                    throw new Exception("خطایی در ذخیره Html وجود دارد");
                this.ConnectionHandler.CommitTransaction();
               return true;
            }
            catch (KnownException knownException)
            {
                this.ConnectionHandler.RollBack();
              throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
               throw new KnownException(ex.Message, ex);
            }
        }




    }
}
