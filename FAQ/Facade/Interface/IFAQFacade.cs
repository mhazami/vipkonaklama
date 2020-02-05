using System.Collections.Generic;
using System.Web;
using Radyn.FAQ.DataStructure;
using Radyn.Framework;

namespace Radyn.FAQ.Facade.Interface
{
public interface IFAQFacade : IBaseFacade<DataStructure.FAQ>
{
    bool Insert(DataStructure.FAQ obj,FAQContent content, HttpPostedFileBase image);
    bool Update(DataStructure.FAQ obj, FAQContent faqContent, HttpPostedFileBase image);
    IEnumerable<DataStructure.FAQ> Search(string value);
}
}
