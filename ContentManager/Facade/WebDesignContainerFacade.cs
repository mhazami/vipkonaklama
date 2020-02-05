using System;
using System.Collections.Generic;
using System.Data;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.ContentManager.BO;
using Radyn.ContentManager.DataStructure;
using Radyn.ContentManager.Facade.Interface;

namespace Radyn.ContentManager.Facade
{
    internal sealed class WebDesignContainerFacade : ContentManagerBaseFacade<WebDesignContainer>, IWebDesignContainerFacade
    {
        internal WebDesignContainerFacade() { }

        internal WebDesignContainerFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                
                var congressContainerBo = new WebDesignContainerBO();
                var obj = congressContainerBo.Get(this.ConnectionHandler, keys);
                if (!congressContainerBo.Delete(this.ConnectionHandler, keys))
                    throw new Exception("خطایی در حذف قالب وجود دارد");
                if (!new ContainerBO().Delete(ConnectionHandler,obj.ContainerId))
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
        public IEnumerable<ContentManager.DataStructure.Container> GetByWebSiteId(Guid websiteId)
        {
            try
            {

               
                return new WebDesignContainerBO().Select(this.ConnectionHandler,x=>x.WebSiteContainer,x=>x.WebId == websiteId);
                
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

        public bool Insert(Guid websiteId, ContentManager.DataStructure.Container container)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
               container.IsExternal = true;
                if (!new ContainerBO().Insert(ConnectionHandler,container))
                    throw new Exception("خطایی در ذخیره قالب وجود دارد");
                var congressContent = new WebDesignContainer { ContainerId = container.Id, WebId = websiteId };
                if (!new WebDesignContainerBO().Insert(this.ConnectionHandler, congressContent))
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
