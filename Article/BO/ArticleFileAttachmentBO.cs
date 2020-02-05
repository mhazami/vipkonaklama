using System;
using Radyn.Article.DataStructure;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Article.BO
{
    internal class ArticleFileAttachmentBO : BusinessBase<ArticleFileAttachment>
    {
        public bool Insert(IConnectionHandler connectionHandler, IConnectionHandler filemamanegrconnectionHandler, ArticleFileAttachment obj)
        {

            var fileTransactionalFacade =
                FileManagerComponent.Instance.FileTransactionalFacade(filemamanegrconnectionHandler);
            var articleFileAttachmentBO = new ArticleFileAttachmentBO();
            fileTransactionalFacade.Insert(obj.File);
           if (!articleFileAttachmentBO.Insert(connectionHandler, obj))
                throw new Exception("خطایی در ذخیره فایل مقاله وجود دارد");

            return true;

        }

        public bool Update(IConnectionHandler connectionHandler, IConnectionHandler filemamanegrconnectionHandler, ArticleFileAttachment obj)
        {

            if (obj.File == null||obj.File.Content==null) return false;
            var fileTransactionalFacade =
                FileManagerComponent.Instance.FileTransactionalFacade(filemamanegrconnectionHandler);
            obj.File.Id = obj.FileId;
            if (!fileTransactionalFacade.Update(obj.File))
                throw new Exception("خطایی در ویرایش فایل مقاله وجود دارد");


            return true;

        }


    }
}
