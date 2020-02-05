using System;
using System.Collections.Generic;
using System.Web;
using Radyn.ContentManager.DataStructure;
using Radyn.Framework;

namespace Radyn.ContentManager.Facade.Interface
{
    public interface IPartialsFacade : IBaseFacade<Partials>
    {
       
      
        List<Partials> GetOperationPartials(Guid OperationId);
        List<Partials> GetContentPartials(string culture);

        List<Partials> GetContentPartials(IEnumerable<int> idlist, string culture);

        bool Insert(Partials supporter, HttpPostedFileBase file);
        bool Update(Partials supporter, HttpPostedFileBase file);
    }
}
