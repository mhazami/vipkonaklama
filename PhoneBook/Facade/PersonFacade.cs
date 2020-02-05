using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Radyn.PhoneBook.BO;
using Radyn.PhoneBook.DataStructure;
using Radyn.PhoneBook.Facade.Interface;
using Radyn.EnterpriseNode;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.PhoneBook.Tools;
using Radyn.Utility;

namespace Radyn.PhoneBook.Facade
{
    internal sealed class PersonFacade : PhoneBookBaseFacade<Person>, IPersonFacade
    {
        internal PersonFacade() { }

        internal PersonFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

       

        public bool Insert(Person person, List<PersonAddress> addresses, List<PersonPhone> personPhones, HttpPostedFileBase file)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.EnterpriseNodeConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new PersonBO().Insert(this.ConnectionHandler,this.EnterpriseNodeConnection, person,addresses,personPhones,file))
                    throw new Exception("خطایی در ذخیره شخص وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                this.EnterpriseNodeConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool Update(Person person,  List<PersonAddress> addresses, List<PersonPhone> personPhones, HttpPostedFileBase file)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.EnterpriseNodeConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new PersonBO().Update(this.ConnectionHandler, this.EnterpriseNodeConnection, person, addresses, personPhones, file))
                    throw new Exception("خطایی در ویرایش شخص وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                this.EnterpriseNodeConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.EnterpriseNodeConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                var obj = new PersonBO().Get(this.ConnectionHandler, keys);
                if (obj == null) return false;
                var personAddressBo = new PersonAddressBO();
                var personAddresses = personAddressBo.Where(this.ConnectionHandler, x => x.PersonId == obj.Id);
                foreach (var personAddress in personAddresses)
                {
                    if (!personAddressBo.Delete(this.ConnectionHandler, personAddress.Id))
                        return false;
                }
                var personPhoneBo = new PersonPhoneBO();
                var personPhones = personPhoneBo.Where(this.ConnectionHandler, x => x.PersonId == obj.Id);
                foreach (var personPhone in personPhones)
                {
                    if (!personPhoneBo.Delete(this.ConnectionHandler, personPhone.Id))
                        return false;
                }
                if (!EnterpriseNodeComponent.Instance.EnterpriseNodeTransactionalFacade(this.EnterpriseNodeConnection).Delete(keys))
                    throw new Exception("خطایی در حذف اطلاعات شخصی  وجود دارد");
                if(!new PersonBO().Delete(this.ConnectionHandler,keys))
                    throw new Exception("خطایی در حذف  شخص  وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                this.EnterpriseNodeConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }



      

        public List<ModelView.PersonSearch> SearchPerson(string textfile)
        {
            try
            {
                var personSearches=new List<ModelView.PersonSearch>();
                var predicateBuilder = new PredicateBuilder<Person>();
                if (!string.IsNullOrEmpty(textfile))
                {
                    predicateBuilder.And(x => x.JopTitle.Contains(textfile) || x.PersonOffice.Title.Contains(textfile) || x.PersonDepartment.Title.Contains(textfile) ||
                                              x.PersoneliCode.Contains(textfile) || (x.EnterpriseNode.RealEnterpriseNode.FirstName + " " + x.EnterpriseNode.RealEnterpriseNode.LastName).Contains(textfile) || x.EnterpriseNode.LegalEnterpriseNode.Title.Contains(textfile) ||
                                              x.EnterpriseNode.Address.Contains(textfile)|| x.EnterpriseNode.Tel.Contains(textfile));

                    var @select = new PersonAddressBO().Select(ConnectionHandler, x => x.PersonId, x => x.Address.Contains(textfile),true);
                    if (select.Any())
                        predicateBuilder.Or(x => x.Id.In(select));
                    var guids = new PersonPhoneBO().Select(ConnectionHandler, x => x.PersonId, x => x.TelNumber.Contains(textfile),true);
                    if (guids.Any())
                        predicateBuilder.Or(x => x.Id.In(guids));
                }
                var list= new PersonBO().Select(this.ConnectionHandler,new Expression<Func<Person, object>>[]
                {
                    x=>x.Id,x=>x.Enabled,x=>x.EnterpriseNode.RealEnterpriseNode.FirstName+" "+x.EnterpriseNode.RealEnterpriseNode.LastName,x=>x.EnterpriseNode.LegalEnterpriseNode.Title,
                    x=>x.PersoneliCode,x=>x.JopTitle,x=>x.EnterpriseNode.Tel,x=>x.EnterpriseNode.PictureId,
                    x=>x.PersonDepartment.Title,x=>x.PersonOffice.Title
                }, predicateBuilder.GetExpression());
                if (!list.Any())
                    return personSearches;
                var enumerable = list.Select(x => (Guid)x.Id);
                var personPhones=new PersonPhoneBO().OrderBy(ConnectionHandler,x=>x.TelNumber,x=>x.PersonId.In(enumerable)&&x.PhoneType.ShowSearchResult);
                var phoneTypes = new PhoneTypeBO().Where(ConnectionHandler, x => x.ShowSearchResult);
                foreach (var obj in list)
                {
                    var personSearch = new ModelView.PersonSearch
                    {
                        Id = obj.Id,
                        Enabled = obj.Enabled is bool ? obj.Enabled : false,
                        PictureId = obj.PictureId is Guid ? obj.PictureId : null,
                        JopTitle = obj.JopTitle is string ? obj.JopTitle : null,
                        PersoneliCode = obj.PersoneliCode is string ? obj.PersoneliCode : null,
                        Name = obj.FirstNameAndLastName is string ? obj.FirstNameAndLastName : obj.Title,
                        DepartmentName = obj.Title1 is string ? obj.Title1 :null,
                        OfficeName = obj.Title2 is string ? obj.Title2 :null,
                        Tel = obj.Tel is string ? obj.Tel : null
                    };
                    var collection = personPhones.Where(x => x.PersonId == personSearch.Id).ToList();
                    foreach (var phoneType in phoneTypes)
                    {
                        var @where = collection.Where(x => x.PhoneTypeId == phoneType.Id);
                        personSearch.PersonPhoneSearches.Add(string.Join(",", @where.Select(x=>x.TelNumber)));
                       
                    }
                    
                    personSearches.Add(personSearch);
                }

                return personSearches.OrderBy(x => x.Name).ToList();
            }
            catch (KnownException ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }

        }
    }
}
