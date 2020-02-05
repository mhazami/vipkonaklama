using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Payment.DataStructure;

namespace Radyn.Payment.DA
{
    public sealed class AccountDA : DALBase<Account>
    {
        public AccountDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class AccountCommandBuilder
    {
    }
}
