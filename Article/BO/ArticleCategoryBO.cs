using System;
using System.Collections.Generic;
using System.Web;
using Radyn.Article.DA;
using Radyn.Article.DataStructure;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Article.BO
{
    internal class ArticleCategoryBO : BusinessBase<ArticleCategory>
    {
        public IEnumerable<ArticleCategory> GetAllByArticleId(IConnectionHandler connectionHandler, Guid aId)
        {
            var articleCategoryDa = new ArticleCategoryDA(connectionHandler);
            return articleCategoryDa.GetAllByArticleId(aId);
        }

        protected override void CheckConstraint(IConnectionHandler connectionHandler, ArticleCategory item)
        {
            base.CheckConstraint(connectionHandler, item);
            if (this.Any(connectionHandler, x => x.Title == item.Title && x.Id != item.Id))
                throw new KnownException(string.Format("دسته بندی با عنوان : {0} قبلا ثبت شده است", item.Title));
        }
        public bool Insert(IConnectionHandler connectionHandler, IConnectionHandler filemamanegrconnectionHandler, DataStructure.ArticleCategory obj, HttpPostedFileBase file)
        {
            if (file != null)
                obj.FileId =
                    FileManagerComponent.Instance.FileTransactionalFacade(filemamanegrconnectionHandler).Insert(file);
            if (!this.Insert(connectionHandler, obj))
                throw new Exception("خطایی در ذخیره گروه مقاله وجود دارد");
           return true;
        }
        public bool Update(IConnectionHandler connectionHandler, IConnectionHandler filemamanegrconnectionHandler, DataStructure.ArticleCategory obj, HttpPostedFileBase file)
        {
            var fileTransactionalFacade =
                FileManagerComponent.Instance.FileTransactionalFacade(filemamanegrconnectionHandler);
            if (file != null)
            {
                if (obj.FileId.HasValue)
                    fileTransactionalFacade.Update(file, (Guid)obj.FileId);
                else
                    obj.FileId = fileTransactionalFacade.Insert(file);
            }
            if (!this.Update(connectionHandler, obj))
                throw new Exception("خطایی در ذخیره گروه مقاله وجود دارد");
            return true;
        }
    }
}
