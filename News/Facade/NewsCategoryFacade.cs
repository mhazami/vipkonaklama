using Radyn.Framework.DbHelper;
using Radyn.News.DataStructure;
using Radyn.News.Facade.Interface;

namespace Radyn.News.Facade
{
    internal sealed class NewsCategoryFacade : NewsBaseFacade<NewsCategory>, INewsCategoryFacade
    {
        internal NewsCategoryFacade()
        {
        }

        internal NewsCategoryFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

      
       
       
       
    }
}
