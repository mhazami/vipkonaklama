using System;
using System.Collections.Generic;
using System.Web;
using Radyn.Framework;
using Radyn.Common.DataStructure;

namespace Radyn.Common.Facade.Interface
{
public interface IWebDesignLanguageFacade : IBaseFacade<WebDesignLanguage>
{
    IEnumerable<Common.DataStructure.Language> GetValidList(Guid websiteId);
    IEnumerable<Common.DataStructure.Language> GetByWebSiteId(Guid websiteId);
    bool Insert(Guid websiteId, Common.DataStructure.Language language, HttpPostedFileBase image);
}
}
