using System;
using System.Collections.Generic;
using System.Web;
using Radyn.Contracts.DAL.UrbanBase;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Article.BO
{
    internal class ArticleBO : BusinessBase<Article.DataStructure.Article>
    {
        public override bool Insert(IConnectionHandler connectionHandler, DataStructure.Article obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref  id);
            obj.Id = id;
            return base.Insert(connectionHandler, obj);
        }

        public IEnumerable<DataStructure.Article> GetByCategoryId(IConnectionHandler connectionHandler,short categoryId, int? topCount)
        {
            var articleDa = new ArticleDA(connectionHandler);
            return articleDa.GetByCategoryId(categoryId, topCount);

        }

        protected override void CheckConstraint(IConnectionHandler connectionHandler, DataStructure.Article item)
        {
            base.CheckConstraint(connectionHandler, item);
            if(this.Any(connectionHandler,x=>x.Title==item.Title&&x.Id!=item.Id))
                throw new KnownException(string.Format("مقاله ای با عنوان : {0} قبلا ثبت شده است",item.Title));
        }

        public bool Insert(IConnectionHandler connectionHandler, IConnectionHandler filemamanegrconnectionHandler, DataStructure.Article obj, HttpPostedFileBase file)
        {
            if (file != null)
                obj.ThumbnailId =
                    FileManagerComponent.Instance.FileTransactionalFacade(filemamanegrconnectionHandler).Insert(file);
            if (!this.Insert(connectionHandler, obj))
                throw new Exception("خطایی در ذخیره مقاله وجود دارد");
            if (obj.ArticleFileAttachments != null)
            {
                var detailsBo = new ArticleFileAttachmentBO();
                foreach (var advertisementDetailse in obj.ArticleFileAttachments)
                {
                    advertisementDetailse.ArticleId = obj.Id;
                    if (!detailsBo.Insert(connectionHandler,filemamanegrconnectionHandler, advertisementDetailse))
                        throw new KnownException("خطا در ثبت اطلاعات");
                }
            }
            return true;
        }
        public bool Update(IConnectionHandler connectionHandler, IConnectionHandler filemamanegrconnectionHandler, DataStructure.Article obj, HttpPostedFileBase file)
        {
            var fileTransactionalFacade =
                FileManagerComponent.Instance.FileTransactionalFacade(filemamanegrconnectionHandler);
            if (file != null)
            {
                if (obj.ThumbnailId.HasValue)
                    fileTransactionalFacade.Update(file, (Guid)obj.ThumbnailId);
                else
                    obj.ThumbnailId = fileTransactionalFacade.Insert(file);
            }
           if (!this.Update(connectionHandler, obj))
                throw new Exception("خطایی در ذخیره مقاله وجود دارد");
            return true;
        }
    }
}
