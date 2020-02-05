using Radyn.FAQ.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.FAQ.DA
{
    public sealed class FAQContentDA : DALBase<FAQContent>
    {
        public FAQContentDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class FAQContentCommandBuilder
    {
    }
}
