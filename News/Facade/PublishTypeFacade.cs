using Radyn.Framework.DbHelper;
using Radyn.News.DataStructure;
using Radyn.News.Facade.Interface;

namespace Radyn.News.Facade
{
    internal sealed class PublishTypeFacade : NewsBaseFacade<PublishType>, IPublishTypeFacade
    {
        internal PublishTypeFacade()
        {
        }

        internal PublishTypeFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

       

     

      
    }
}
