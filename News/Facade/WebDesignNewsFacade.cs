using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.News.DataStructure;
using Radyn.News.BO;
using Radyn.News.Facade.Interface;

namespace Radyn.News.Facade
{
    internal sealed class WebDesignNewsFacade : NewsBaseFacade<DataStructure.WebDesignNews>, IWebDesignNewsFacade
    {
        internal WebDesignNewsFacade() { }

        internal WebDesignNewsFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }


        public bool Insert(Guid websiteId, News.DataStructure.News news, NewsContent newsContent, NewsProperty newsproperty, HttpPostedFileBase file)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                news.IsExternal = true;
                if (!new NewsBO().Insert(ConnectionHandler, FileManagerConnection,news, newsContent, newsproperty, file))
                    throw new Exception("خطایی درذخیره اخبار وجود دارد");
                var congressNews = new Radyn.News.DataStructure.WebDesignNews { NewsId = news.Id, WebId = websiteId };
                if (!new WebDesignNewsBO().Insert(this.ConnectionHandler, congressNews))
                    throw new Exception("خطایی درذخیره اخبار وجود دارد");
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

        public IEnumerable<News.DataStructure.News> TopCount(Guid websiteId, int? top)
        {
            try
            {
                var enumerable = new WebDesignNewsBO().Where(ConnectionHandler, news => news.WebId == websiteId);
                var list = new List<News.DataStructure.News>();
                foreach (var newse in enumerable)
                {
                    if (newse.WebSiteNews.Enabled)
                        list.Add(newse.WebSiteNews);
                }
                var outlist = list.OrderByDescending(news => news.PublishDate).ThenByDescending(x => x.PublishTime);
                return top != null && top > 0 ? outlist.Take((int)top) : outlist;
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

        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                
                var obj = new WebDesignNewsBO().Get(this.ConnectionHandler, keys);
                if (!new NewsBO().Delete(this.ConnectionHandler, FileManagerConnection, keys))
                    throw new Exception("خطایی در حذف اخبار وجود دارد");
                if (!new NewsBO().Delete(ConnectionHandler,obj.NewsId))
                    throw new Exception("خطایی در حذف اخبار وجود دارد");
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
