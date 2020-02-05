using System;
using Radyn.Common.BO;
using Radyn.Common.DataStructure;
using Radyn.Common.Facade.Interface;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Common.Facade
{
    internal sealed class ProvinceFacade : CommonBase<Province>, IProvinceFacade
    {
        internal ProvinceFacade()
        {
        }

        internal ProvinceFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

        
        public override bool Delete(params object[] keys)
        {
            try
            {
                var provinceBo = new ProvinceBO();
                var obj = provinceBo.Get(this.ConnectionHandler, keys);
                if (obj == null) return false;
                var byFilter = new CityBO().Any(this.ConnectionHandler, x => x.ProvinceId == obj.Id);
                if (byFilter)
                    throw new Exception("این کشور دارای استان است آن را نمیتوانید حذف کنید");
                return provinceBo.Delete(this.ConnectionHandler, keys);
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
