using System;
using System.Web;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Gallery.DA;
using Radyn.Utility;

namespace Radyn.Gallery.BO
{
    internal class GalleryBO : BusinessBase<DataStructure.Gallery>
    {
        public override bool Insert(IConnectionHandler connectionHandler, DataStructure.Gallery obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            obj.CreateDate = DateTime.Now.ShamsiDate();
            return base.Insert(connectionHandler, obj);
        }
        public bool Insert(IConnectionHandler connectionHandler, IConnectionHandler FileManagerConnection, DataStructure.Gallery obj, HttpPostedFileBase image)
        {
           
                
                if (image != null)
                    obj.Thumbnail =
                        FileManagerComponent.Instance.FileTransactionalFacade(FileManagerConnection).Insert(image);
                if (!this.Insert(connectionHandler, obj))
                    throw new Exception("خطایی در ذخیره گالری وجود دارد");

               
                return true;
           
        }

        public override bool Delete(IConnectionHandler connectionHandler, params object[] keys)
        {
            var obj = new GalleryBO().Get(connectionHandler, keys);
            var photos = new PhotoBO().Where(connectionHandler, photo => photo.GalleryId == obj.Id);
            var photoFacade = new PhotoBO();
            foreach (var pic in photos)
            {
                if (!photoFacade.Delete(connectionHandler,pic.Id))
                    throw new Exception("خطایی در حذف عکس وجود دارد");
            }
            if (!base.Delete(connectionHandler, keys))
                throw new Exception("خطایی در حذف گالری وجود دارد");
            return true;
        }

        public bool HasPhoto(IConnectionHandler connectionHandler, Guid id)
        {
            var da = new GalleryDA(connectionHandler);
            return da.HasPhoto(id)>0;
        }

        public bool HasChild(IConnectionHandler connectionHandler, Guid id)
        {
            var da = new GalleryDA(connectionHandler);
            return da.HasChild(id) > 0;
        }

       
    }
}
