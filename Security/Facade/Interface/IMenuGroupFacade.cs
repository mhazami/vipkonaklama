using System;
using System.Collections.Generic;
using System.Web;
using Radyn.Framework;
using Radyn.Security.DataStructure;

namespace Radyn.Security.Facade.Interface
{
public interface IMenuGroupFacade : IBaseFacade<MenuGroup>
{
    bool Insert(MenuGroup operation,List<Guid>  menulistId,HttpPostedFileBase fileBase);
    bool Update(MenuGroup operation,List<Guid>  menulistId,HttpPostedFileBase fileBase);
}
}
