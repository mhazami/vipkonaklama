using System.Web;
using Radyn.Article.DataStructure;
using Radyn.Framework;

namespace Radyn.Article.Facade.Interface
{
    public interface IArticleCategoryFacade : IBaseFacade<ArticleCategory>
    {

        bool Insert(DataStructure.ArticleCategory obj, HttpPostedFileBase file);
        bool Update(DataStructure.ArticleCategory obj, HttpPostedFileBase file);
    }
}
