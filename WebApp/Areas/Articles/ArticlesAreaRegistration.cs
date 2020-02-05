using System.Web.Mvc;

namespace Radyn.WebApp.Areas.Articles
{
    public class ArticlesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Articles";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "DefaultArticle",
                "Articles/Article/{id}",
                 new { controller = "Article", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "DefaultArticleList",
               "Articles/ArticleList/{id}",
                 new { controller = "ArticleList", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Articles_default",
                "Articles/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Articles_default2",
                "Articles/{controller}/{action}/{id}/{*catchall}",
                new { action = "Index", id = UrlParameter.Optional }
            );
            
        }
    }
}
