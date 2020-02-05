using Radyn.Framework.DbHelper;
using Radyn.Help.DataStructure;
using Radyn.Help.Facade.Interface;

namespace Radyn.Help.Facade
{
    internal sealed class HelpReationCollectionFacade : HelpFacade<HelpReationCollection>, IHelpReationCollectionFacade
    {
        internal HelpReationCollectionFacade() { }

        internal HelpReationCollectionFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

    }
}
