using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.PhoneBook.DataStructure;

namespace Radyn.PhoneBook.DA
{
    public sealed class AddressTypeDA : DALBase<AddressType>
    {
        public AddressTypeDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        { }
    }
    internal class AddressTypeCommandBuilder
    {
    }
}
