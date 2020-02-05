using Radyn.ContentManager.DataStructure;
using Radyn.ContentManager.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.ContentManager.Facade
{
    internal sealed class ContentContentFacade : ContentManagerBaseFacade<ContentContent>, IContentContentFacade
    {
        internal ContentContentFacade() { }

        internal ContentContentFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

       
    }
}
