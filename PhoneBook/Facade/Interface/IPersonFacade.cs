using Radyn.Framework;
using Radyn.PhoneBook.DataStructure;
using System.Collections.Generic;
using System.Web;
using Radyn.PhoneBook.Tools;

namespace Radyn.PhoneBook.Facade.Interface
{
    public interface IPersonFacade : IBaseFacade<Person>
    {
        bool Insert(Person person,  List<PersonAddress> addresses, List<PersonPhone> personPhones, HttpPostedFileBase file);
        bool Update(Person person,  List<PersonAddress> addresses, List<PersonPhone> personPhones, HttpPostedFileBase file);
       
        List<ModelView.PersonSearch> SearchPerson(string textfile);
    }
}
