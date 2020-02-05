using System.Collections.Generic;
using System.Linq;
using System.Web;
using Radyn.PhoneBook.DataStructure;
using Radyn.EnterpriseNode;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.PhoneBook.BO
{
    internal class PersonBO : BusinessBase<Person>
    {
        public bool Insert(IConnectionHandler connectionHandler, IConnectionHandler exterprisenodeconnection, Person person, List<PersonAddress> addresses, List<PersonPhone> personPhones, HttpPostedFileBase file)
        {

            var id = person.Id;
            BOUtility.GetGuidForId(ref id);
            person.Id = id;
            person.EnterpriseNode.Id = person.Id;
            if (!EnterpriseNodeComponent.Instance.EnterpriseNodeTransactionalFacade(exterprisenodeconnection).Insert(person.EnterpriseNode, file))
                return false;
            if (!this.Insert(connectionHandler, person))
                return false;
            foreach (var personPhone in personPhones)
            {
                personPhone.PersonId = person.Id;
                if (!new PersonPhoneBO().Insert(connectionHandler, personPhone)) return false;
            }
            foreach (var personAddress in addresses)
            {
                personAddress.PersonId = person.Id;
                if (!new PersonAddressBO().Insert(connectionHandler, personAddress)) return false;
            }
            return true;

        }

        internal bool Update(IConnectionHandler connectionHandler, IConnectionHandler exterprisenodeconnection, Person person, List<PersonAddress> addresses, List<PersonPhone> personPhones, HttpPostedFileBase file)
        {
            if (!EnterpriseNodeComponent.Instance.EnterpriseNodeTransactionalFacade(exterprisenodeconnection).Update(person.EnterpriseNode, file))
                return false;
            if (!this.Update(connectionHandler, person))
                return false;
            var personAddressBo = new PersonAddressBO();
            var personAddresses = personAddressBo.Where(connectionHandler, x => x.PersonId == person.Id);
            foreach (var personAddress in personAddresses)
            {
                if (!addresses.Any(address => address.Id.Equals(personAddress.Id)))
                {
                    if (!personAddressBo.Delete(connectionHandler, personAddress.Id))
                        return false;
                }
            }
            foreach (var personAddress in addresses)
            {
                var firstOrDefault = personAddresses.FirstOrDefault(address => address.Id.Equals(personAddress.Id));
                if (firstOrDefault == null)
                {
                    personAddress.PersonId = person.Id;
                    if (!personAddressBo.Insert(connectionHandler, personAddress)) return false;
                }
                if (!personAddressBo.Update(connectionHandler, personAddress)) return false;
            }
            var personPhoneBo = new PersonPhoneBO();
            var phones = personPhoneBo.Where(connectionHandler, x => x.PersonId == person.Id);
            foreach (var personPhone in phones)
            {
                if (!personPhones.Any(phone => phone.Id.Equals(personPhone.Id)))
                {
                    if (!personPhoneBo.Delete(connectionHandler, personPhone.Id))return false;
                }
            }
            foreach (var personPhone in personPhones)
            {
                var firstOrDefault = phones.FirstOrDefault(phone => phone.Id.Equals(personPhone.Id));
                if (firstOrDefault == null)
                {
                    personPhone.PersonId = person.Id;
                    if (!personPhoneBo.Insert(connectionHandler, personPhone)) return false;
                }
                if (!personPhoneBo.Update(connectionHandler, personPhone)) return false;

            }
            return true;
        }


    }
}
