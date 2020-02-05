using System.Linq;
using System.Web.Mvc;
using Radyn.News;
using Radyn.News.DataStructure;
using Radyn.WebApp.AppCode.Base;

namespace Radyn.WebApp.Areas.News.Controllers
{
    public class NewsPropertyController : WebDesignBaseController
    {
        public ActionResult Modify(string state, int newsid=0)
        {
            var newsContentTypes = NewsComponent.Instance.NewsContentTypeFacade.GetAll().ToList();
            ViewBag.NewsContentType = new SelectList(newsContentTypes, "Id", "Title");
            switch (state)
            {

                case "Modify":
                    {
                        var property = new NewsProperty();
                        if (newsid == 0) return PartialView("Modify", property);
                        property = NewsComponent.Instance.NewsPropertyFacade.Get(newsid);
                        if (property != null)
                        {
                            ViewBag.Files =
                                NewsComponent.Instance.AttachedFileFacade.Where(file => file.NewsId == (int) newsid)
                                    .Select(file => file.File);
                        }
                        return PartialView("Modify", property);
                    }

                case "Details":
                    {
                        var newsProperty = NewsComponent.Instance.NewsPropertyFacade.Get(newsid);
                        if (newsProperty != null)
                            return PartialView("DetailInfo", newsProperty);
                    }
                    break;

            }

            return null;
        }


    }
}
