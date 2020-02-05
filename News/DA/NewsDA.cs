using System;
using System.Collections.Generic;
using System.Linq;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.News.DA
{
    public sealed class NewsDA : DALBase<News.DataStructure.News>
    {
        public NewsDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

        public IEnumerable<DataStructure.News> GetByCategory(Guid categoryId, int? topCount)
        {
            var newsCommandBuilder = new NewsCommandBuilder();
            var query = newsCommandBuilder.GetByCategory(categoryId, topCount);
            return DBManager.GetCollection<DataStructure.News>(base.ConnectionHandler, query);
        }

        public IEnumerable<DataStructure.News> GetTopNews(int topCount, bool isSelected)
        {
            var newsCommandBuilder = new NewsCommandBuilder();
            var query = newsCommandBuilder.GetTopNews(topCount, isSelected);
            return DBManager.GetCollection<DataStructure.News>(base.ConnectionHandler, query);
        }

        public IEnumerable<DataStructure.News> Search(string text)
        {
            var newsCommandBuilder = new NewsCommandBuilder();
            var query = newsCommandBuilder.Search(text);
            return DBManager.GetCollection<DataStructure.News>(base.ConnectionHandler, query);
        }
    }
    internal class NewsCommandBuilder
    {
        public string GetByCategory(Guid categoryId, int? topCount)
        {
            return string.Format(
                 "select distinct {1}* from News.News where NewsCategoryId='{0}' order by PublishDate desc ,PublishTime desc",
                 categoryId, topCount != null ? "top(" + topCount + ")" : "");
        }

        public string GetTopNews(int topCount, bool isSelected)
        {
            var query = string.Format("SELECT  distinct  top({0})  News.News.* " +
                                      " FROM         News.News INNER JOIN " +
                                      " News.NewsProperty ON News.News.Id = News.NewsProperty.Id", topCount);
            string where = "News.News.Enabled=1 and ";
            if (isSelected) where += "News.NewsProperty.IsSelection=1 and ";
            where = where.Substring(0, where.Length - 4);
            return string.Format("{0} where {1} order by News.News.PublishDate desc ,News.News.PublishTime desc", query, where);
        }
        public string Search(string text)
        {
            const string query = "SELECT   distinct  News.News.*" +
                                 " FROM         News.News INNER JOIN " +
                                 " News.NewsContent ON News.News.Id = News.NewsContent.Id";
            var where = "";
            var split = text.Split();
            var str = split.Aggregate("", (current, value) => current + string.Format("News.NewsContent.Title1 like N'%{0}%' or ", value));
            str = str.Substring(0, str.Length - 3);
            where += string.Format("({0}) OR ", str);

            var strbody = split.Aggregate("", (current, value) => current + string.Format("News.NewsContent.Title2 like N'%{0}%' or ", value));
            strbody = strbody.Substring(0, strbody.Length - 3);
            where += string.Format("({0}) or ", strbody);

            var OverTitle = split.Aggregate("", (current, value) => current + string.Format("News.NewsContent.OverTitle like N'%{0}%' or ", value));
            OverTitle = OverTitle.Substring(0, OverTitle.Length - 3);
            where += string.Format("({0}) or ", OverTitle);

            var Lead = split.Aggregate("", (current, value) => current + string.Format("News.NewsContent.Lead like N'%{0}%' or ", value));
            Lead = Lead.Substring(0, Lead.Length - 3);
            where += string.Format("({0}) or ", Lead);

            var Sutitr = split.Aggregate("", (current, value) => current + string.Format("News.NewsContent.Sutitr like N'%{0}%' or ", value));
            Sutitr = Sutitr.Substring(0, Sutitr.Length - 3);
            where += string.Format("({0}) or ", Sutitr);



            where = where.Substring(0, where.Length - 3);
            return string.Format("{0} where {1} order by News.News.PublishDate desc ,News.News.PublishTime desc", query, where);

        }
    }
}
