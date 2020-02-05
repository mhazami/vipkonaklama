using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Help.DataStructure;
using Radyn.Security.DataStructure;

namespace Radyn.Help.Facade.Interface
{
    public interface IHelpMenuFacade : IBaseFacade<HelpMenu>
    {
        IEnumerable<Menu> SearchAddedInHelp(Guid helpId, string value);
    }
}
