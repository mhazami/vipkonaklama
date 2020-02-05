using System.Collections.Generic;
using System.Web;
using Radyn.Common.DataStructure;
using Radyn.Framework;

namespace Radyn.Common.Facade.Interface
{
    public interface ILanguageFacade : IBaseFacade<Language>
    {
        bool Insert(Language obj, HttpPostedFileBase logo);
        bool Update(Language obj, HttpPostedFileBase logo);
        List<Language> GetValidList();
    }
}
