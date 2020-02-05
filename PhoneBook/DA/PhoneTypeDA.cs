using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.PhoneBook.DataStructure;

namespace Radyn.PhoneBook.DA
{
    public sealed class PhoneTypeDA : DALBase<PhoneType>
    {
        public PhoneTypeDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class PhoneTypeCommandBuilder
    {
    }
}
