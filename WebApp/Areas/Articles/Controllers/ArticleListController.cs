using Radyn.Article;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Radyn.WebApp.Areas.Articles.Controllers
{
    public class ArticleListController : WebDesignBaseController
    {


        public ActionResult Index(string id, string keyword = null)
        {
            if (string.IsNullOrEmpty(id)) return View();

            var articleCategory = ArticleComponent.Instance.ArticleCategoryFacade.FirstOrDefault(x => x.Title == id);
            if (articleCategory == null) return View();
            var builder = new PredicateBuilder<Article.DataStructure.Article>();
            builder.And(x => x.ArticleCategoryId == articleCategory.Id && x.Enable);
            if (!string.IsNullOrEmpty(keyword))
                builder.And(x => x.Keyword.Contains(keyword));
            var @where = ArticleComponent.Instance.ArticleFacade.OrderBy(x => x.Order, builder.GetExpression());
            if (!@where.Any())
            {
                ShowMessage(String.Format("مقاله ای در گروه {0} ثبت نشده است", articleCategory.Title, "", MessageIcon.Error));
                return Redirect("/Articles/CategoryList");
            }

            if (where.Count == 1)
                return Redirect("/Articles/Article/" + where.FirstOrDefault().Title);
            ViewBag.PageTitle = articleCategory.Title;
            return View(@where);
        }

        public ActionResult PVIndex(string id)
        {
            var categories = ArticleComponent.Instance.ArticleCategoryFacade.Count();
            //if (categories == 1)
            //{
            id = ArticleComponent.Instance.ArticleCategoryFacade.SelectFirstOrDefault(x => x.Id).ToString();
            //}
            //if (string.IsNullOrEmpty(id)) return PartialView("PVIndex");

            var articleCategory = ArticleComponent.Instance.ArticleCategoryFacade.FirstOrDefault(x => x.Id == id.ToInt());
            //if (articleCategory == null) return View();
            //var builder = new PredicateBuilder<Article.DataStructure.Article>();
            //builder.And(x => x.ArticleCategoryId == articleCategory.Id && x.Enable);

            var @where = ArticleComponent.Instance.ArticleFacade.OrderBy(x => x.Order, x => x.ArticleCategoryId == articleCategory.Id);
            if (!@where.Any())
            {
                ShowMessage(String.Format("مقاله ای در گروه {0} ثبت نشده است", articleCategory.Title, "", MessageIcon.Error));
                return Redirect("/Articles/CategoryList");
            }

            if (where.Count == 1)
                return Redirect("/Articles/Article/" + where.FirstOrDefault().Title);
            ViewBag.PageTitle = articleCategory.Title;
            return PartialView("PVIndex", @where);
        }
    }
}
