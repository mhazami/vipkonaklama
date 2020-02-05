using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Help.DataStructure;

namespace Radyn.Help.Facade.Interface
{
    public interface IHelpFacade : IBaseFacade<Help.DataStructure.Help>
    {

        bool Insert(DataStructure.Help obj, HelpContent content);
        bool AddHelpRelation(Guid sourceId, List<Guid> destinationIdList);
        IEnumerable<DataStructure.Help> GetHelpRelation(Guid sourceId);
        bool DeleteHelpRealtion(Guid sourceId, Guid relationid);
        IEnumerable<Help.DataStructure.Help> Search(string txt);
        bool AddMenu(Guid opId, List<Guid> list);
    }
}
