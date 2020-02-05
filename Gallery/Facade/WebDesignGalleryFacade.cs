using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Gallery.BO;
using Radyn.Gallery.Facade.Interface;

namespace Radyn.Gallery.Facade
{
    internal sealed class WebDesignGalleryFacade : GalleryBaseFacade<DataStructure.WebDesignGallery>, IWebDesignGalleryFacade
    {
        internal WebDesignGalleryFacade() { }

        internal WebDesignGalleryFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }


        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                var galleryBo = new WebDesignGalleryBO();
                var obj = galleryBo.Get(this.ConnectionHandler, keys);
                if (!galleryBo.Delete(this.ConnectionHandler, keys))
                    throw new Exception("خطایی در حذف گالری وجود دارد");
                if (!new GalleryBO().Delete(ConnectionHandler,obj.GalleryId))
                    throw new Exception("خطایی در حذف گالری وجود دارد");
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
        public IEnumerable<Gallery.DataStructure.Gallery> GetParents(Guid websiteId)
        {
            try
            {

                return new WebDesignGalleryBO().Select(ConnectionHandler, x => x.WebSiteGallery, x => x.WebSiteGallery.ParentGallery == null && x.WebId == websiteId);


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

        public bool Insert(Guid websiteId, Gallery.DataStructure.Gallery gallery, HttpPostedFileBase image)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                gallery.IsExternal = true;
                gallery.CreateDate = Utility.DateTimeUtil.ShamsiDate(DateTime.Now);
                if (!new GalleryBO().Insert(ConnectionHandler, FileManagerConnection,gallery, image))
                    throw new Exception("خطایی درذخیره گالری وجود دارد");
                var congessGallery = new DataStructure.WebDesignGallery { GalleryId = gallery.Id, WebId = websiteId };
                if (!new WebDesignGalleryBO().Insert(this.ConnectionHandler, congessGallery))
                    throw new Exception("خطایی درذخیره گالری وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException knownException)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                throw new KnownException(ex.Message, ex);
            }
        }
    }
}
