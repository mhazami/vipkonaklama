using System;
using System.Collections.Generic;
using System.Web;
using Radyn.EnterpriseNode.DAL;
using Radyn.EnterpriseNode.Tools;
using Radyn.FileManager;
using Radyn.FileManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.EnterpriseNode.BO
{
    public class EnterpriseNodeBO : BusinessBase<EnterpriseNode.DataStructure.EnterpriseNode>
    {
        public override bool Update(IConnectionHandler connectionHandler, DataStructure.EnterpriseNode obj)
        {
            if ((obj.LegalEnterpriseNode != null && obj.RealEnterpriseNode != null) ||
                   (obj.LegalEnterpriseNode == null && obj.RealEnterpriseNode == null))
                throw new KnownException("نوع شخص (حقیقی/حقوقی) قابل شناسایی نمی باشد.");
            var id = obj.Id;
            if (id.Equals(Guid.Empty))
                id = Guid.NewGuid();
            obj.Id = id;
            if (obj.LegalEnterpriseNode != null)
            {
                obj.LegalEnterpriseNode.Id = id;
                if (!new LegalEnterpriseNodeBO().Update(connectionHandler, obj.LegalEnterpriseNode))
                    throw new KnownException("خطا در ثبت اطلاعات شخص حقوقی");
            }
            if (obj.RealEnterpriseNode != null)
            {
                obj.RealEnterpriseNode.Id = id;
                if (!new RealEnterpriseNodeBO().Update(connectionHandler, obj.RealEnterpriseNode))
                    throw new KnownException("خطا در ثبت اطلاعات شخص حقیقی");
            }
            return base.Update(connectionHandler, obj);
        }

        public override bool Insert(IConnectionHandler connectionHandler, DataStructure.EnterpriseNode obj)
        {
            if ((obj.LegalEnterpriseNode != null && obj.RealEnterpriseNode != null) ||
                    (obj.LegalEnterpriseNode == null && obj.RealEnterpriseNode == null))
                throw new KnownException("نوع شخص (حقیقی/حقوقی) قابل شناسایی نمی باشد.");
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            if (obj.LegalEnterpriseNode != null)
                obj.EnterpriseNodeTypeId = Enums.EnterpriseNodeType.LegalEnterPriseNode;
            if (obj.RealEnterpriseNode != null)
                obj.EnterpriseNodeTypeId = Enums.EnterpriseNodeType.RealEnterPriseNode;

            if (!base.Insert(connectionHandler, obj))
                throw new KnownException("خطا در ثبت اطلاعات شخص");
            if (obj.LegalEnterpriseNode != null)
            {
                obj.LegalEnterpriseNode.Id = id;
                if (!new LegalEnterpriseNodeBO().Insert(connectionHandler, obj.LegalEnterpriseNode))
                    throw new KnownException("خطا در ثبت اطلاعات شخص حقوقی");
            }
            if (obj.RealEnterpriseNode != null)
            {
                obj.RealEnterpriseNode.Id = id;
                if (!new RealEnterpriseNodeBO().Insert(connectionHandler, obj.RealEnterpriseNode))
                    throw new KnownException("خطا در ثبت اطلاعات شخص حقیقی");
            }
            return true;
        }

        public bool Insert(IConnectionHandler connectionHandler,IConnectionHandler filemanagerconnection, DataStructure.EnterpriseNode obj, HttpPostedFileBase file)
        {
            if (file != null)
                obj.PictureId = FileManagerComponent.Instance.FileTransactionalFacade(filemanagerconnection).Insert(file, new File(){MaxSize = 200});
            if(!this.Insert(connectionHandler, obj))
                throw new KnownException("خطا در ثبت اطلاعات شخص");
            return true;
        }
        public bool Update(IConnectionHandler connectionHandler, IConnectionHandler filemanagerconnection, DataStructure.EnterpriseNode obj, HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (obj.PictureId.HasValue)
                {
                    if (!FileManagerComponent.Instance.FileTransactionalFacade(filemanagerconnection).Update(file, (Guid)obj.PictureId, new File() { MaxSize = 200 }))
                        throw new Exception("خطایی در ویرایش عکس وجود دارد");
                }
                else
                    obj.PictureId = FileManagerComponent.Instance.FileTransactionalFacade(filemanagerconnection).Insert(file);
            }
            if (obj.PictureId != null) return this.Update(connectionHandler, obj);
            if (!this.Update(connectionHandler, obj)) return false;
            var oldobj = this.Get(connectionHandler, obj.Id);
            return !oldobj.PictureId.HasValue || FileManagerComponent.Instance.FileFacade.Delete(oldobj.PictureId);
          
        }

        public bool Delete(IConnectionHandler connectionHandler, IConnectionHandler filemanagerconnection, DataStructure.EnterpriseNode enterpriseNode)
        {
            if (enterpriseNode == null) return false;
            if (!base.Delete(connectionHandler, enterpriseNode.Id))
                throw new KnownException("خطا در حذف اطلاعات شخص");
            if (enterpriseNode.PictureId.HasValue)
            {
                if (!FileManagerComponent.Instance.FileTransactionalFacade(filemanagerconnection).Delete(enterpriseNode.PictureId))
                    throw new Exception("خطایی در حذف عکس وجود دارد");
            }
            return true;
        }

        public List<DataStructure.EnterpriseNode> Search(IConnectionHandler connectionHandler, DataStructure.EnterpriseNode filter)
        {
            var da = new EnterpriseNodeDA(connectionHandler);
            return da.Search(filter);
        }
        public List<DataStructure.EnterpriseNode> Search(IConnectionHandler connectionHandler, string filter, Enums.EnterpriseNodeType enterpriseNodeTypeId = Enums.EnterpriseNodeType.RealEnterPriseNode)
        {
            var da = new EnterpriseNodeDA(connectionHandler);
            return da.Search(filter, enterpriseNodeTypeId);
        }
      

      
        
    }
}
