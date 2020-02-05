using System;
using System.Collections.Generic;
using Radyn.ContentManager.DataStructure;
using Radyn.Framework;

namespace Radyn.ContentManager.Facade.Interface
{
    public interface IHtmlDesginFacade : IBaseFacade<HtmlDesgin>
    {
        Dictionary<string, string> ReturnCustomeAttributes(Guid htmdesignId);
       




    }
}
