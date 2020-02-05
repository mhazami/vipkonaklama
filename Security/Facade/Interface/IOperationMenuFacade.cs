using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Security.DataStructure;

namespace Radyn.Security.Facade.Interface
{
public interface IOperationMenuFacade : IBaseFacade<OperationMenu>
{
    List<Menu> GetOprationMenu(Guid oprationId, Guid ? userId, bool? display = true);
    List<Menu> GetOprationMenu(Guid oprationId, int groupId,Guid ? userId, bool? display = true);
   
   
}
}
