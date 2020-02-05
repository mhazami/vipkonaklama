using System;
using System.Collections.Generic;
using Radyn.Article.DataStructure;
using Radyn.Framework;

namespace Radyn.Article.Facade.Interface
{
    public interface IArticleResourceFacade : IBaseFacade<ArticleResource>
    {
       
        IEnumerable<ArticleResource> GetAllByArticleId(Guid aId);
        
    }
}
