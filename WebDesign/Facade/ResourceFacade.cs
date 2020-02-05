using System;
using System.Data;
using System.Web;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Utility;
using Radyn.WebDesign.BO;
using Radyn.WebDesign.DataStructure;
using Radyn.WebDesign.Definition;
using Radyn.WebDesign.Facade.Interface;

namespace Radyn.WebDesign.Facade
{
    internal sealed class ResourceFacade : WebDesignBaseFacade<Resource>, IResourceFacade
    {
        internal ResourceFacade() { }

        internal ResourceFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
        public string GetWebSiteResourceHtml(Guid congress, string culture, Enums.UseLayout layout)
        {
            try
            {

                return new ResourceBO().GetWebSiteResourceHtml(ConnectionHandler, congress, culture, layout);
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


        public bool Insert(Resource resource, HttpPostedFileBase file)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (file != null)
                {

                    resource.FileId =
                        FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection)
                            .Insert(file, new FileManager.DataStructure.File() { MaxSize = 200 }).ToString();
                }

                if (!new ResourceBO().Insert(this.ConnectionHandler, resource))
                    throw new Exception("  خطایی در ذخیره resource  وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                this.ContentManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException knownException)
            {
                this.ConnectionHandler.RollBack();
                this.ContentManagerConnection.RollBack();
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.ContentManagerConnection.RollBack();
                throw new KnownException(ex.Message, ex);
            }
        }

        public override bool Delete(Resource obj)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);

                var fileTransactionalFacade =
                    FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection);

                var lanuageContent = new ResourceBO().GetLanuageContent(base.ConnectionHandler,
                    obj.CurrentUICultureName, obj.Id);
                if (!string.IsNullOrEmpty(lanuageContent.FileId))
                {
                    fileTransactionalFacade.Delete(lanuageContent.FileId);
                }




                if (!new ResourceBO().Delete(this.ConnectionHandler, obj))
                    throw new Exception("  خطایی در حذف resource  وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                this.ContentManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException knownException)
            {
                this.ConnectionHandler.RollBack();
                this.ContentManagerConnection.RollBack();
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.ContentManagerConnection.RollBack();
                throw new KnownException(ex.Message, ex);
            }
        }



        public bool Update(Resource resource, HttpPostedFileBase resourceFile)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (resourceFile != null)
                {
                    var fileTransactionalFacade =
                        FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection);

                    var lanuageContent = new ResourceBO().GetLanuageContent(base.ConnectionHandler,
                        resource.CurrentUICultureName, resource.Id);
                    if (!string.IsNullOrEmpty(lanuageContent.FileId))
                    {
                        fileTransactionalFacade
                            .Update(resourceFile, lanuageContent.FileId.ToGuid());
                    }
                    else
                    {
                        resource.FileId =
                            fileTransactionalFacade
                                .Insert(resourceFile, new FileManager.DataStructure.File() { MaxSize = 200 }).ToString();
                    }


                }
                if (!new ResourceBO().Update(this.ConnectionHandler, resource))
                    throw new Exception("  خطایی در ویرایش resource  وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                this.ContentManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException knownException)
            {
                this.ConnectionHandler.RollBack();
                this.ContentManagerConnection.RollBack();
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.ContentManagerConnection.RollBack();
                throw new KnownException(ex.Message, ex);
            }
        }
    }
}
