using Radyn.Framework.DbHelper;
using Radyn.Help.DataStructure;
using Radyn.Help.Facade.Interface;

namespace Radyn.Help.Facade
{
    internal sealed class HelpContentFacade : HelpFacade<HelpContent>, IHelpContentFacade
    {
        internal HelpContentFacade() { }

        internal HelpContentFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

       
    }
}
