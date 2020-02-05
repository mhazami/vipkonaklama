using System;
using System.Collections.Generic;
using System.Data;
using Radyn.ContentManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.ContentManager.BO;
using Radyn.ContentManager.Facade.Interface;
using WebDesignContent = Radyn.ContentManager.DataStructure.WebDesignContent;

namespace Radyn.ContentManager.Facade
{
    internal sealed class WebDesignContentFacade : ContentManagerBaseFacade<WebDesignContent>, IWebDesignContentFacade
    {
        internal WebDesignContentFacade() { }

        internal WebDesignContentFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }


        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
               var congressContentBo = new WebDesignContentBO();
                var obj = congressContentBo.Get(this.ConnectionHandler, keys);
                if (!congressContentBo.Delete(this.ConnectionHandler, keys))
                    throw new Exception("خطایی در حذف محتوا وجود دارد");
                if (!new ContentBO().Delete(ConnectionHandler,obj.ContentId))
                    throw new Exception("خطایی در حذف محتوا وجود دارد");
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
        public IEnumerable<ContentManager.DataStructure.Content> GetByWebSiteId(Guid webSiteId, bool onlyenabled)
        {
            try
            {
                
               return new WebDesignContentBO().Select(this.ConnectionHandler,x=>x.WebSiteContent,x=>x.WebId == webSiteId&&x.WebSiteContent.Enabled);
              
              
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

        public bool Insert(Guid websiteId, ContentManager.DataStructure.Content content, ContentContent contentcontent)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                content.IsExternal = true;
                if (!new ContentBO().Insert(ConnectionHandler,content, contentcontent))
                    throw new Exception("خطایی در ذخیره محتوا وجود دارد");
                var congressContent = new WebDesignContent { ContentId = content.Id, WebId = websiteId };
                if (!new WebDesignContentBO().Insert(this.ConnectionHandler, congressContent))
                    throw new Exception("خطایی در ذخیره محتوا وجود دارد");
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
