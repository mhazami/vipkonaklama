using System;
using System.Collections.Generic;
using System.Data;
using Radyn.ContentManager.BO;
using Radyn.ContentManager.DataStructure;
using Radyn.ContentManager.Facade.Interface;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.ContentManager.Facade
{
    internal class WebDesignPagesFacade : ContentManagerBaseFacade<WebDesignPages>, IWebDesignPagesFacade
    {
        internal WebDesignPagesFacade()
        {
        }

        internal WebDesignPagesFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);

                var congressContainerBo = new WebDesignPagesBO();
                var obj = congressContainerBo.Get(this.ConnectionHandler, keys);
                if (!congressContainerBo.Delete(this.ConnectionHandler, keys))
                    throw new Exception("خطایی در حذف قالب وجود دارد");
                if (!new PagesBO().Delete(ConnectionHandler, obj.Pages))
                    throw new Exception("خطایی در حذف قالب وجود دارد");
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
        public IEnumerable<ContentManager.DataStructure.Pages> GetByWebSiteId(Guid websiteId)
        {
            try
            {


                return new WebDesignPagesBO().Select(this.ConnectionHandler, x => x.Pages, x => x.WebId == websiteId);

            }
            catch (KnownException knownException)
            {

                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {

                throw new KnownException(ex.Message, ex);
            }
        }

        public bool Insert(Guid websiteId, ContentManager.DataStructure.Pages pages, HtmlDesgin htmlDesgin)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new PagesBO().Insert(ConnectionHandler, pages, htmlDesgin))
                    throw new Exception("خطایی در ذخیره قالب وجود دارد");
                var congressContent = new WebDesignPages() { PagesId = pages.Id, WebId = websiteId };
                if (!new WebDesignPagesBO().Insert(this.ConnectionHandler, congressContent))
                    throw new Exception("خطایی در ذخیره قالب وجود دارد");
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
