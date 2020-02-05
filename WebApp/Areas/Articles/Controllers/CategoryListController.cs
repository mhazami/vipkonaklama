using System;
using System.Linq;
using System.Web.Mvc;
using Radyn.Article;
using Radyn.WebApp.AppCode.Base;

namespace Radyn.WebApp.Areas.Articles.Controllers
{
    public class CategoryListController : WebDesignBaseController
    {

     
        public ActionResult Index()
        {
            try
            {

                var where = ArticleComponent.Instance.ArticleCategoryFacade.OrderBy(x => x.Order, x => x.Enable);
                if (!where.Any())
                {
                    ShowMessage("گروه مقاله ای ثبت نشده است");
                    return View();
                }
                if (where.Count == 1)
                    return Redirect("/Articles/ArticleList/" + where.FirstOrDefault().Title);

                return View(@where);
            }
            catch (Exception ex)
            {

                ShowExceptionMessage(ex);
                return View();
            }

        }
       
    }
}
