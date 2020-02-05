using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.PhoneBook.DataStructure;

namespace Radyn.PhoneBook.DA
{
    public sealed class PersonAddressDA : DALBase<PersonAddress>
    {
        public PersonAddressDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        { }
    }
    internal class PersonAddressCommandBuilder
    {
    }
}
