using System.Collections.Generic;
using System.Web;
using Radyn.Framework;

namespace Radyn.Article.Facade.Interface
{
    public interface IArticleFacade : IBaseFacade<Article.DataStructure.Article>
    {
     
        bool Insert(DataStructure.Article obj, HttpPostedFileBase file);
        bool Update(DataStructure.Article obj,HttpPostedFileBase file);
        IEnumerable<DataStructure.Article> GetByCategoryId(short categoryId, int? topCount=null);
    }
}
