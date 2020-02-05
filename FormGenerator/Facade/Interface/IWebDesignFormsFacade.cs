using System;
using Radyn.FormGenerator.DataStructure;
using Radyn.Framework;

namespace Radyn.FormGenerator.Facade.Interface
{
public interface IWebDesignFormsFacade : IBaseFacade<WebDesignForms>
{
    bool Insert(Guid websiteId, FormStructure formStructure);
    bool UpdateAndAssgine(Guid WebId, FormStructure structure, string urls, bool forUser);
}
}
