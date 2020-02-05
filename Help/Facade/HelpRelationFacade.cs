using Radyn.Framework.DbHelper;
using Radyn.Help.DataStructure;
using Radyn.Help.Facade.Interface;

namespace Radyn.Help.Facade
{
    internal sealed class HelpRelationFacade : HelpFacade<HelpRelation>, IHelpRelationFacade
    {
        internal HelpRelationFacade() { }

        internal HelpRelationFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

       
    }
}
