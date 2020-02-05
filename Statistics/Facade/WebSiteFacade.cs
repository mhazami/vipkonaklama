using System;
using System.Data;
using System.Linq;
using System.Web;
using Radyn.EnterpriseNode;
using Radyn.FileManager;
using Radyn.FileManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Statistics.BO;
using Radyn.Statistics.DataStructure;
using Radyn.Statistics.Facade.Interface;

namespace Radyn.Statistics.Facade
{
    internal sealed class WebSiteFacade : StatisticsBaseFacade<WebSite>, IWebSiteFacade
    {
        internal WebSiteFacade()
        {
        }

        internal WebSiteFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

        

        public bool Insert(WebSite obj, EnterpriseNode.DataStructure.EnterpriseNode enterPrise, HttpPostedFileBase file)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.EnterpriseNodeConnection.StartTransaction(IsolationLevel.ReadCommitted);
                obj.Url = obj.Url.ToLower();
                var webSiteBo = new WebSiteBO();
                var byUrl = webSiteBo.FirstOrDefault(this.ConnectionHandler, x => x.Url.ToLower() == obj.Url.ToLower());
                if (byUrl != null)
                    throw new Exception("وب سایتی با این مسیر قبلا ثبت شده است");
                if (
                    !EnterpriseNodeComponent.Instance.EnterpriseNodeTransactionalFacade(this.EnterpriseNodeConnection)
                        .Insert(enterPrise))
                    throw new Exception("خطایی در ذخیره اطلاعات وب سایت وجود دارد");
                obj.OwnerId = enterPrise.Id;
                if (!webSiteBo.Insert(this.ConnectionHandler, obj))
                    throw new Exception("خطایی در ذخیره وب سایت وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                this.EnterpriseNodeConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool Update(WebSite obj, EnterpriseNode.DataStructure.EnterpriseNode enterPrise, HttpPostedFileBase file)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.EnterpriseNodeConnection.StartTransaction(IsolationLevel.ReadCommitted);
                var webSiteBo = new WebSiteBO();
                var byUrl = webSiteBo.FirstOrDefault(this.ConnectionHandler,x=>x.Url.ToLower()== obj.Url.ToLower());
                if (byUrl!=null&&byUrl.Id != obj.Id)
                    throw new Exception("وب سایتی با این مسیر قبلا ثبت شده است");
                if (
                    !EnterpriseNodeComponent.Instance.EnterpriseNodeTransactionalFacade(this.EnterpriseNodeConnection)
                        .Update(enterPrise, file))
                    throw new Exception("خطایی در ویرایش اطلاعات وب سایت وجود دارد");
                obj.Url = obj.Url.ToLower();
                if (!webSiteBo.Update(this.ConnectionHandler, obj))
                    throw new Exception("خطایی در ویرایش وب سایت وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                this.EnterpriseNodeConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.EnterpriseNodeConnection.StartTransaction(IsolationLevel.ReadCommitted);
                var webSiteBo = new WebSiteBO();
                var obj = webSiteBo.Get(this.ConnectionHandler, keys);
                if (!webSiteBo.Delete(this.ConnectionHandler, keys))
                    throw new Exception("خطایی در حذف وب سایت وجود دارد");
                if (
                    !EnterpriseNodeComponent.Instance.EnterpriseNodeTransactionalFacade(this.EnterpriseNodeConnection)
                        .Delete(obj.OwnerId))
                    throw new Exception("خطایی در حذف اطلاعات وب سایت وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                this.EnterpriseNodeConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        
        public Guid GetByUrlForEditor(string authority)
        {
            try
            {
                var guid = Guid.Empty;
                var webSiteBo = new WebSiteBO();
                WebSite webSite=null;
                var all = webSiteBo.GetAll(ConnectionHandler);
                if (all.Any())
                {
                     webSite= string.IsNullOrEmpty(authority)? null: webSiteBo.FirstOrDefault(this.ConnectionHandler, x => x.Url.ToLower() == authority.ToLower());
                   
                   
                }
                if (webSite != null)
                {
                    var folder = FileManagerComponent.Instance.FolderFacade.Get(webSite.Id);
                    if (folder == null)
                    {
                        folder = new Folder {Id = webSite.Id, Title = authority, IsExternal = true};
                        if (FileManagerComponent.Instance.FolderFacade.Insert(folder))
                            guid = folder.Id;
                    }
                    else
                        guid = folder.Id;
                }
                return guid;
            }
            catch (KnownException ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool Modify(string url, string title, Guid owner)
        {
            try
            {
                var webSiteBo = new WebSiteBO();
                var byUrl = webSiteBo.FirstOrDefault(this.ConnectionHandler, x => x.Url.ToLower() ==url.ToLower());
                if (byUrl == null)
                {
                    var webSite = new WebSite {Url = url.ToLower(), Title = title, OwnerId = owner};
                    if (!webSiteBo.Insert(this.ConnectionHandler, webSite))
                        throw new Exception("خطایی در ذخیره وب سایت وجود دارد");
                }
                else
                {
                    byUrl.OwnerId = owner;
                    byUrl.Url = url.ToLower();
                    byUrl.Title = title;
                    if (!webSiteBo.Update(this.ConnectionHandler, byUrl))
                        throw new Exception("خطایی در ویرایش وب سایت وجود دارد");
                }


                return true;
            }
            catch (KnownException ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }
    }
}
