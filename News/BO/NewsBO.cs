using System;
using System.Collections.Generic;
using System.Web;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.News.DataStructure;
using Radyn.News.DA;

namespace Radyn.News.BO
{
    internal class NewsBO : BusinessBase<News.DataStructure.News>
    {
        public IEnumerable<DataStructure.News> GetByCategory(IConnectionHandler connectionHandler, Guid categoryId, int? topCount)
        {
            var newsDa = new NewsDA(connectionHandler);
            return newsDa.GetByCategory(categoryId, topCount);
        }

        public IEnumerable<DataStructure.News> GetTopNews(IConnectionHandler connectionHandler, int topCount, bool isSelected)
        {
            var newsDa = new NewsDA(connectionHandler);
            return newsDa.GetTopNews(topCount, isSelected);
        }

        public IEnumerable<DataStructure.News> Search(IConnectionHandler connectionHandler, string text)
        {
            var newsDa = new NewsDA(connectionHandler);
            return newsDa.Search(text);
        }
        public bool Insert(IConnectionHandler connectionHandler, IConnectionHandler FileManagerConnection, DataStructure.News news, NewsContent content, NewsProperty property,
         HttpPostedFileBase fileBase)
        {

            if (fileBase != null)
                news.ThumbnailId =
                    FileManagerComponent.Instance.FileTransactionalFacade(FileManagerConnection)
                        .Insert(fileBase);
            if (!this.Insert(connectionHandler, news))
                throw new Exception("خطایی در ذخیره اخبار وجود دارد");
            if (content != null)
            {
                content.Id = news.Id;
                if (!new NewsContentBO().Insert(connectionHandler, content))
                    throw new Exception("خطایی در ذخیره محتوای اخبار وجود دارد");
            }
            if (property != null)
            {
                property.Id = news.Id;
                if (!new NewsPropertyBO().Insert(connectionHandler, property))
                    throw new Exception("خطایی در ذخیره تنظیمات اخبار وجود دارد");
            }

            return true;

        }

        public  bool Delete(IConnectionHandler connectionHandler, IConnectionHandler FileManagerConnection, params object[] keys)
        {

            var newsBO = new NewsBO();
            var obj = newsBO.Get(connectionHandler, keys);
            var newsContentBO = new NewsContentBO();
            var content = newsContentBO.Where(connectionHandler,
                newsContent => newsContent.Id == obj.Id);
            foreach (var newsContent in content)
            {
                if (!newsContentBO.Delete(connectionHandler, newsContent.Id, newsContent.LanguageId))
                    throw new Exception("خطایی در حذف محتوای اخبار وجود دارد");
            }
            var newsPropertyBO = new NewsPropertyBO();
            var firstOrDefault =
                newsPropertyBO.FirstOrDefault(connectionHandler, property => property.Id == obj.Id);
            if (firstOrDefault != null)
            {
                if (!newsPropertyBO.Delete(connectionHandler, firstOrDefault.Id))
                    throw new Exception("خطایی در حذف تنظیمات اخبار وجود دارد");
            }
            if (!newsBO.Delete(connectionHandler, keys))
                throw new Exception("خطایی در حذف اخبار وجود دارد");
            if (obj.ThumbnailId != null)
                FileManagerComponent.Instance.FileTransactionalFacade(FileManagerConnection)
                    .Delete(obj.ThumbnailId);
            return true;
        }

        public override bool Update(IConnectionHandler connectionHandler, DataStructure.News obj)
        {
            if (!base.Update(connectionHandler, obj)) return false;
            var oldobj = this.Get(connectionHandler, obj.Id);
            if (oldobj.ThumbnailId.HasValue && obj.ThumbnailId == null)
                FileManagerComponent.Instance.FileFacade.Delete(oldobj.ThumbnailId);
            return true;

        }
    }
}
