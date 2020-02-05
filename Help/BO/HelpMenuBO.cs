using System;
using System.Collections.Generic;
using System.Linq;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Help.DataStructure;
using Radyn.Security;
using Radyn.Security.DataStructure;
using Radyn.Utility;

namespace Radyn.Help.BO
{
    internal class HelpMenuBO : BusinessBase<HelpMenu>
    {
        public IEnumerable<Menu> SearchAddedInHelp(IConnectionHandler connectionHandler, Guid helpId, string value)
        {
            var predicateBuilder = new PredicateBuilder<Menu>();
            var guids = this.Select(connectionHandler, x => x.MenuId, x => x.HelpId == helpId, true);
            if (guids.Any())
                predicateBuilder.And(x => x.Id.In(guids));
            predicateBuilder.And(x => x.Enabled);
            if (!string.IsNullOrEmpty(value))
                predicateBuilder.And(x => x.Url.Contains(value) || x.Title.Contains(value));
            return SecurityComponent.Instance.MenuFacade.OrderBy(x => x.Order, predicateBuilder.GetExpression());
        }
    }
}
