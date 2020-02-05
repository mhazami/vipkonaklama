using Radyn.Common.DA;
using Radyn.Common.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Utility;

namespace Radyn.Common.BO
{
internal class CountryIPRangeBO : BusinessBase<CountryIPRange>
{
    public string GetIpCulture(IConnectionHandler connectionHandler, string ipAddress)
    {
        var countryIpRangeDa = new CountryIPRangeDA(connectionHandler);
        return countryIpRangeDa.GetIpCulture(FixIpRange(ipAddress));
    }

    protected override void CheckConstraint(IConnectionHandler connectionHandler, CountryIPRange item)
    {

        item.StartRange = FixIpRange(item.StartRange);
        item.EndRange = FixIpRange(item.EndRange);
     
        base.CheckConstraint(connectionHandler, item);
    }
    public string FixIpRange(string Ip)
    {
        var txt = "";
        var ipsplit = Ip.Split('.');
        for (int i = 0; i <= 3; i++)
        {
            txt += string.Format("{0:D3}", ipsplit[i].ToInt());
            if (i < 3) txt += ".";
        }
        return txt;
    }
}
}
