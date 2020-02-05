using System;
using System.Collections.Generic;
using Radyn.ContentManager.DataStructure;
using Radyn.Framework;

namespace Radyn.ContentManager.Facade.Interface
{
    public interface IWebDesignHtmlFacade : IBaseFacade<WebDesignHtml>
    {
      
        bool Insert(Guid websiteId, HtmlDesgin htmlDesgin);
       List<Partials> GetWebDesignContent(Guid websiteId, string culture);

    }
}
