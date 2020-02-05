using System;
using Radyn.Common.BO;
using Radyn.Common.DataStructure;
using Radyn.Common.Facade.Interface;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Utility;

namespace Radyn.Common.Facade
{
    internal sealed class CountryIPRangeFacade : CommonBase<CountryIPRange>, ICountryIPRangeFacade
    {
        internal CountryIPRangeFacade()
        {
        }

        internal CountryIPRangeFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }



        public bool ValidateIp(CountryIPRange ip)
        {

            var startIp = ip.StartRange;
            var endIp = ip.EndRange;
            if (string.IsNullOrEmpty(startIp) || string.IsNullOrEmpty(endIp)) return false;
            var startsplit = startIp.Split('.');
            var endsplit = endIp.Split('.');
            for (int i = 0; i <= 3; i++)
            {
                var StringStart = "";
                var StringEnd = "";
                if (startsplit[i].Length > 3) return false;
                StringStart += string.Format("{0:D3}", startsplit[i].ToInt());
                if (endsplit[i].Length > 3) return false;
                StringEnd += string.Format("{0:D3}", endsplit[i].ToInt());
                if (StringStart.CompareTo("255") > 0 || StringEnd.CompareTo("255") > 0) return false;
                if (i == 0)
                {
                    if (StringStart.CompareTo("001") < 0 || StringStart.CompareTo("223") > 0) return false;
                    if (StringEnd.CompareTo("001") < 0 || StringEnd.CompareTo("223") > 0) return false;
                }
                if (StringStart.CompareTo(StringEnd) > 0) return false;
            }
            return true;

        }

        public string GetByIpAddress(string ipAddress)
        {
            try
            {

                if (string.IsNullOrEmpty(ipAddress))
                {
                    var defaultCulture = CommonComponent.GetDefaultCulture;
                    return defaultCulture;
                }
                var byIpAddress = new CountryIPRangeBO().GetIpCulture(this.ConnectionHandler, ipAddress);
                return byIpAddress;
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
