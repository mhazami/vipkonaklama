using System;
using System.Collections.Generic;
using System.Web;
using Radyn.FAQ.DataStructure;
using Radyn.Framework;

namespace Radyn.FAQ.Facade.Interface
{
public interface IWebDesignFAQFacade : IBaseFacade<DataStructure.WebDesignFAQ>
{
    IEnumerable<FAQ.DataStructure.FAQ> GetByWebSiteId(Guid websiteId);
    IEnumerable<FAQ.DataStructure.FAQ> Search(Guid id, string value);
    bool Insert(Guid websiteId, FAQ.DataStructure.FAQ faq, FAQContent faqContent, HttpPostedFileBase image);
}
}
