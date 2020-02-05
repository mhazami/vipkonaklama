using System;
using System.Collections.Generic;
using System.Web;
using Radyn.ContentManager.DataStructure;
using Radyn.Framework;

namespace Radyn.ContentManager.Facade.Interface
{
    public interface IDefaultHtmlDesginFacade : IBaseFacade<DefaultHtmlDesgin>
    {


        bool Insert(DefaultHtmlDesgin htmlDesgin, HttpPostedFileBase file);
        bool Update(DefaultHtmlDesgin htmlDesgin, HttpPostedFileBase file);



    }
}
