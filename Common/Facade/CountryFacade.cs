using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Radyn.Common.BO;
using Radyn.Common.DataStructure;
using Radyn.Common.Facade.Interface;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Common.Facade
{
    internal sealed class CountryFacade : CommonBase<Country>, ICountryFacade
    {
        internal CountryFacade()
        {
        }

        internal CountryFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

      

        public bool Insert(Country country, List<CountryIPRange> list, HttpPostedFileBase file)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (file != null)
                    country.Image =
                        FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection).Insert(file);
                if (!new CountryBO().Insert(this.ConnectionHandler, country))
                    throw new Exception("خطایی در ذخیره کشور وجود دارد");
                var countryIpRangeBo = new CountryIPRangeBO();

                foreach (var countryIpRange in list)
                {
                    countryIpRange.CountryId = country.Id;
                    if (!countryIpRangeBo.Insert(this.ConnectionHandler, countryIpRange))
                        throw new Exception("خطایی در ذخیره  IP کشور وجود دارد");
                }
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                var countryBo = new CountryBO();
                var obj = countryBo.Get(this.ConnectionHandler, keys);
                if (obj == null) return false;
                var countryIpRangeBo = new CountryIPRangeBO();
                var byFilter = new ProvinceBO().Any(this.ConnectionHandler, x => x.CountryId == obj.Id);
                if (byFilter)
                    throw new Exception("این کشور دارای استان است آن را نمیتوانید حذف کنید");
                var countryIpRanges = countryIpRangeBo.Where(this.ConnectionHandler, x => x.CountryId == obj.Id);
                foreach (var countryIpRange in countryIpRanges)
                {
                    if (!countryIpRangeBo.Delete(this.ConnectionHandler, countryIpRange.Id))
                        throw new Exception("خطایی در حذف  IP کشور وجود دارد");
                }

                if (!countryBo.Delete(this.ConnectionHandler, keys))
                    throw new Exception("خطایی در حذف کشور وجود دارد");
                if (obj.Image != null)
                    FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection).Delete(obj.Image);
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool Update(Country country, List<CountryIPRange> list, HttpPostedFileBase file)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);

                if (file != null)
                {
                    var fileTransactionalFacade =
                        FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection);
                    if (country.Image.HasValue)
                        fileTransactionalFacade
                            .Update(file, (Guid) country.Image);
                    else
                        country.Image =
                            fileTransactionalFacade
                                .Insert(file);
                }


                var countryIpRangeBo = new CountryIPRangeBO();
                var countryIpRanges = countryIpRangeBo.Where(this.ConnectionHandler, x => x.CountryId == country.Id);
                foreach (var countryIpRange in list)
                {
                    CountryIPRange range = countryIpRange;
                    var ipRange = countryIpRangeBo.FirstOrDefault(this.ConnectionHandler, x =>
                    
                        x.CountryId == country.Id&&
                        x.Id == range.Id);
                    if (ipRange == null)
                    {
                        countryIpRange.CountryId = country.Id;
                        if (!countryIpRangeBo.Insert(this.ConnectionHandler, countryIpRange))
                            throw new Exception("خطایی در ذخیره  IP کشور وجود دارد");
                    }
                    else
                    {
                        if (!countryIpRangeBo.Update(this.ConnectionHandler, countryIpRange))
                            throw new Exception("خطایی در ذخیره  IP کشور وجود دارد");
                    }

                }
                foreach (var countryIpRange in countryIpRanges)
                {
                    if (list.Any(x => x.Id == countryIpRange.Id)) continue;
                    if (!countryIpRangeBo.Delete(this.ConnectionHandler, countryIpRange.Id))
                        throw new Exception("خطایی در حذف  IP کشور وجود دارد");
                }
                if (!new CountryBO().Update(this.ConnectionHandler, country))
                    throw new Exception("خطایی در ذخیره کشور وجود دارد");

                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }
    }
}
