using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

using Radyn.News;
using Radyn.News.Tools;
using Radyn.Utility;
using Radyn.WebApp.AppCode.Security;
using Radyn.WebApp.AppCode.Base;

namespace Radyn.WebApp.Areas.News.Controllers
{
    public class UIPanelController : WebDesignBaseController
    {

        public ActionResult GetNewsList(List<Radyn.News.DataStructure.News> newses)
        {
            
            return PartialView("PVNewsList", newses);
        }

        
        public ActionResult GetNews(List<Radyn.News.DataStructure.News> newses)
        {
           
            return PartialView("PVNews", newses);
        }
       
        public ActionResult NewsView(Int32 Id)
        {
            var WebDesignNews = NewsComponent.Instance.NewsFacade.Get(this.WebSite.Id, Id);
            if (WebDesignNews == null) return View(new Radyn.News.DataStructure.News());
            WebDesignNews.NewsContent = WebDesignNews.GetNewsContent(SessionParameters.Culture);
            return View(WebDesignNews);
        }



        public ActionResult Archives(Guid? groupId)
        {

            var news = NewsComponent.Instance.NewsFacade.Where(x => x.NewsCategoryId == groupId && x.Enabled);
            if (news.Count == 0)
            {
                ShowMessage(Resources.News.This_Category_Not_have_News);
                return null;
            }

            var groupe = NewsComponent.Instance.NewsCategoryFacade.Get(groupId);
            TempData["GroupeName"] = groupe.Title;
            ViewBag.groupId = groupId;
            return View("WebDesignNewsList", news.OrderByDescending(x => x.PublishDate).ToList());
        }




        public ActionResult WebDesignNewsList()
        {
            var news = NewsComponent.Instance.WebDesignNewsFacade.TopCount(this.WebSite.Id, null);
            var newses = news as IList<Radyn.News.DataStructure.News> ?? news.ToList();
            if (newses.Any())
            {
                var @where =
                    NewsComponent.Instance.NewsContentFacade.Where(
                        x => x.Id.In(newses.Select(i => i.Id)) && x.LanguageId == SessionParameters.Culture);
                foreach (var newse in newses)
                {
                    newse.NewsContent = where.FirstOrDefault(x => x.Id == newse.Id);
                }
                return View(newses.ToList());
            }

            ShowMessage(Resources.News.This_Category_Not_have_News);
            return null;

        }
        public ActionResult GetWebDesignNews()
        {
            var maxNewsCountShow = this.WebSite.Configuration.MaxNewsShow;
            var enumerable = NewsComponent.Instance.WebDesignNewsFacade.TopCount(this.WebSite.Id, maxNewsCountShow);
            return PartialView("PVWebDesignNews", enumerable);
        }

    }

}
