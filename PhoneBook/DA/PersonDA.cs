using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.PhoneBook.DataStructure;

namespace Radyn.PhoneBook.DA
{
    public sealed class PersonDA : DALBase<Person>
    {
        public PersonDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        { }

        
    }
    internal class PersonCommandBuilder
    {
       
    }
}
