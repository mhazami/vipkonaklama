
using System;
using Radyn.Framework;
using Radyn.ContentManager.DataStructure;

namespace Radyn.ContentManager.Facade.Interface
{
    public interface IWebDesignMenuHtmlFacade : IBaseFacade<WebDesignMenuHtml>
    {
        bool Insert(Guid congressId, Radyn.ContentManager.DataStructure.MenuHtml htmlDesgin);
      

       

        

    }
}
