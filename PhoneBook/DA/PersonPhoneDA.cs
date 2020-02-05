using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.PhoneBook.DataStructure;

namespace Radyn.PhoneBook.DA
{
    public sealed class PersonPhoneDA : DALBase<PersonPhone>
    {
        public PersonPhoneDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        { }
    }
    internal class PersonPhoneCommandBuilder
    {
    }
}
