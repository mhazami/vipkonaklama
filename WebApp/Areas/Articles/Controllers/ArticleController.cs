using System;
using System.Web.Mvc;
using Radyn.Article;
using Radyn.WebApp.AppCode.Base;

namespace Radyn.WebApp.Areas.Articles.Controllers
{
    public class ArticleController : WebDesignBaseController
    {

        public ActionResult Index(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) return View();
                var articleCategory = ArticleComponent.Instance.ArticleFacade.FirstOrDefault(x => x.Title == id);
                return View(articleCategory);
            }
            catch (Exception ex)
            {

                ShowExceptionMessage(ex);
                return View();
            }
        }
    }
}
