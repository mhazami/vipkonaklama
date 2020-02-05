using System;
using System.Collections.Generic;
using System.Web;
using Radyn.Framework;
using Radyn.News.DataStructure;

namespace Radyn.News.Facade.Interface
{
    public interface IWebDesignNewsFacade : IBaseFacade<DataStructure.WebDesignNews>
    {
        bool Insert(Guid websiteId, News.DataStructure.News news, NewsContent newsContent, NewsProperty newsproperty, HttpPostedFileBase file);
        IEnumerable<News.DataStructure.News> TopCount(Guid websiteId, int? top);
    }
}
