using System;
using System.Collections.Generic;
using System.Web;
using Radyn.Framework;
using Radyn.News.DataStructure;

namespace Radyn.News.Facade.Interface
{
    public interface INewsFacade : IBaseFacade<News.DataStructure.News>
    {
        bool Insert(DataStructure.News news, NewsContent content, NewsProperty property, HttpPostedFileBase fileBase);
        bool Update(DataStructure.News news, NewsContent content, NewsProperty property, HttpPostedFileBase fileBase);
        IEnumerable<DataStructure.News> GetByCategory(Guid categoryId, int? topCount = null);
        IEnumerable<DataStructure.News> GetTopNews(int topCount = 0, bool isSelected = true);
        IEnumerable<DataStructure.News> Search(string text);
    }
}
