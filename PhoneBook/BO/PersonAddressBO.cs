using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.PhoneBook.DataStructure;

namespace Radyn.PhoneBook.BO
{
    internal class PersonAddressBO : BusinessBase<PersonAddress>
    {
        public override bool Insert(IConnectionHandler connectionHandler, PersonAddress obj)
        {
            System.Guid id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            return base.Insert(connectionHandler, obj);
        }
    }
}
