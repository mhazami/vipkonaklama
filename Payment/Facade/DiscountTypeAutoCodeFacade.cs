using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Payment.BO;
using Radyn.Payment.DataStructure;
using Radyn.Payment.Facade.Interface;

namespace Radyn.Payment.Facade
{
    internal sealed class DiscountTypeAutoCodeFacade : PaymentBaseFacade<DiscountTypeAutoCode>, IDiscountTypeAutoCodeFacade
    {
        internal DiscountTypeAutoCodeFacade() { }
        internal DiscountTypeAutoCodeFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
        public List<DiscountTypeAutoCode> GenerateAutoCode(Guid discounttypeId, int count, int characterCount)
        {
            try
            {
                return new DiscountTypeAutoCodeBO().GenerateAutoCode(this.ConnectionHandler, discounttypeId, count, characterCount);
            }
            catch (KnownException ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }
    }
}
