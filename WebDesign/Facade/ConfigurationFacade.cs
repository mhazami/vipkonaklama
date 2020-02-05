using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Utility;
using Radyn.WebDesign.BO;
using Radyn.WebDesign.DataStructure;
using Radyn.WebDesign.Facade.Interface;
using System;
using System.Data;
using System.Web;

namespace Radyn.WebDesign.Facade
{
    internal sealed class ConfigurationFacade : WebDesignBaseFacade<Configuration>, IConfigurationFacade
    {
        internal ConfigurationFacade() { }

        internal ConfigurationFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
        public string FillTempAdditionalData(Guid congressId)
        {
            try
            {
                return new ConfigurationBO().FillTempAdditionalData(this.ConnectionHandler, congressId);
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
        public bool Insert(Configuration configuration, HttpPostedFileBase favIcon)
        {
            try
            {
                ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);

                FileManager.Facade.Interface.IFileFacade fileTransactionalFacade =
                        FileManagerComponent.Instance.FileTransactionalFacade(FileManagerConnection);

                if (favIcon != null)
                {
                    configuration.FavIcon = fileTransactionalFacade.Insert(favIcon);
                }

                if (!new ConfigurationBO().Insert(ConnectionHandler, configuration))
                {
                    throw new Exception("خطای در ذخیره اطلاعات رخ داده است");
                }

                ConnectionHandler.CommitTransaction();
                FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                ConnectionHandler.RollBack();
                FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                ConnectionHandler.RollBack();
                FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool Update(Configuration configuration, HttpPostedFileBase favIcon)
        {
            try
            {
                ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                FileManager.Facade.Interface.IFileFacade fileTransactionalFacade =
                     FileManagerComponent.Instance.FileTransactionalFacade(FileManagerConnection);
                ConfigurationBO configurationBo = new ConfigurationBO();


                Configuration oldconfig = configurationBo.Get(ConnectionHandler, configuration.WebSite.Id);
                configuration.TerminalPassword = !string.IsNullOrEmpty(configuration.TerminalPassword)
                    ? StringUtils.Encrypt(configuration.TerminalPassword)
                    : oldconfig.TerminalPassword;
                configuration.SMSAccountPassword = !string.IsNullOrEmpty(configuration.SMSAccountPassword)
                    ? StringUtils.Encrypt(configuration.SMSAccountPassword)
                    : oldconfig.SMSAccountPassword;
                configuration.MailPassword = !string.IsNullOrEmpty(configuration.MailPassword)
                    ? StringUtils.Encrypt(configuration.MailPassword)
                    : oldconfig.MailPassword;
                configuration.CertificatePassword = !string.IsNullOrEmpty(configuration.CertificatePassword)
                    ? StringUtils.Encrypt(configuration.CertificatePassword)
                    : oldconfig.CertificatePassword;

                configuration.CRMServicePassword = !string.IsNullOrEmpty(configuration.CRMServicePassword)
                    ? StringUtils.Encrypt(configuration.CRMServicePassword)
                    : oldconfig.CRMServicePassword;
                //---------------------
                configuration.MerchantPublicKey = !string.IsNullOrEmpty(configuration.MerchantPublicKey)
                    ? StringUtils.Encrypt(configuration.MerchantPublicKey)
                    : oldconfig.MerchantPublicKey;
                configuration.MerchantPrivateKey = !string.IsNullOrEmpty(configuration.MerchantPrivateKey)
                    ? StringUtils.Encrypt(configuration.MerchantPrivateKey)
                    : oldconfig.MerchantPrivateKey;
                //------------------------


                if (favIcon != null)
                {
                    if (configuration.FavIcon == null)
                    {
                        configuration.FavIcon = fileTransactionalFacade.Insert(favIcon);
                    }
                    else
                    {
                        if (!fileTransactionalFacade.Update(favIcon, (Guid)configuration.FavIcon))
                        {
                            throw new Exception("خطا در ذخیره fav icon وجود دارد");
                        }
                    }
                }
                if (!configurationBo.Update(ConnectionHandler, configuration))
                {
                    throw new Exception("خطای در ذخیره اطلاعات رخ داده است");
                }

                ConnectionHandler.CommitTransaction();
                FileManagerConnection.CommitTransaction();

                return true;
            }
            catch (KnownException ex)
            {
                ConnectionHandler.RollBack();
                FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                ConnectionHandler.RollBack();
                FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }


    }
}
