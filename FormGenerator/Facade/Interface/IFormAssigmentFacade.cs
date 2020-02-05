using System;
using System.Collections.Generic;
using Radyn.FormGenerator.DataStructure;
using Radyn.Framework;

namespace Radyn.FormGenerator.Facade.Interface
{
public interface IFormAssigmentFacade : IBaseFacade<FormAssigment>
{
    bool Insert(Guid formId, List<FormAssigment> list);
   FormAssigment GetByUrl(string url);
   
}
}
