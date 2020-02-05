using Radyn.Framework.DbHelper;
using Radyn.FormGenerator.DataStructure;
using Radyn.FormGenerator.Facade.Interface;

namespace Radyn.FormGenerator.Facade
{
    internal sealed class WebDesignUserFormsFacade : FormGeneratorBaseFacade<WebDesignUserForms>, IWebDesignUserFormsFacade
    {
        internal WebDesignUserFormsFacade()
        {
        }

        internal WebDesignUserFormsFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

       
    }
}
