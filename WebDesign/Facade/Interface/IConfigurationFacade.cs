using System;
using System.Web;
using Radyn.Framework;
using Radyn.WebDesign.DataStructure;

namespace Radyn.WebDesign.Facade.Interface
{
    public interface IConfigurationFacade : IBaseFacade<Configuration>
    {
        bool Insert(Configuration configuration, HttpPostedFileBase favIcon);

        bool Update(Configuration configuration, HttpPostedFileBase favIcon);

         string FillTempAdditionalData(Guid Webid);

    }
}
