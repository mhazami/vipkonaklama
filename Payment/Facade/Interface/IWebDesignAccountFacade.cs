using System;
using Radyn.Framework;

namespace Radyn.Payment.Facade.Interface
{
    public interface IWebDesignAccountFacade : IBaseFacade<DataStructure.WebDesignAccount>
    {
        bool Insert(Guid congressId, Radyn.Payment.DataStructure.Account account);
    }
}
