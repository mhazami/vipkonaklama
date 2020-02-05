using Radyn.ContentManager.DataStructure;
using Radyn.ContentManager.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.ContentManager.Facade
{
    internal sealed class SectionFacade : ContentManagerBaseFacade<Section>, ISectionFacade
    {
        internal SectionFacade()  { }

        internal SectionFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

      
    }
}
