using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Common.BO;
using Radyn.Common.DataStructure;
using Radyn.Common.Facade.Interface;

namespace Radyn.Common.Facade
{
    internal sealed class WebDesignLanguageFacade : CommonBase<WebDesignLanguage>, IWebDesignLanguageFacade
    {
        internal WebDesignLanguageFacade() { }

        internal WebDesignLanguageFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                var congressContentBo = new WebDesignLanguageBO();
                if (!congressContentBo.Delete(this.ConnectionHandler, keys))
                    throw new Exception("خطایی در حذف زبان وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException knownException)
            {
                this.ConnectionHandler.RollBack();
                
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                
                throw new KnownException(ex.Message, ex);
            }
        }
        public IEnumerable<Common.DataStructure.Language> GetValidList(Guid websiteId)
        {
            try
            {
               return new WebDesignLanguageBO().Select(this.ConnectionHandler, x => x.WebSiteLanguage, x => x.WebId == websiteId&&x.WebSiteLanguage.Enabled);
               
                

            }
            catch (KnownException knownException)
            {

                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {

                throw new KnownException(ex.Message, ex);
            }
        }

        public IEnumerable<Common.DataStructure.Language> GetByWebSiteId(Guid websiteId)
        {
            try
            {
               
               return new WebDesignLanguageBO().Select(this.ConnectionHandler, x => x.WebSiteLanguage, x => x.WebId == websiteId);
               

            }
            catch (KnownException knownException)
            {

                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {

                throw new KnownException(ex.Message, ex);
            }
        }

        public bool Insert(Guid websiteId, Common.DataStructure.Language language, HttpPostedFileBase image)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
               
                string langId;
                var haslanguage =new LanguageBO().Get(ConnectionHandler,language.Id);
                if (haslanguage == null)
                {
                    if (!new LanguageBO().Insert(ConnectionHandler, FileManagerConnection,language, image))
                        throw new Exception("خطایی در ذخیره زبان وجود دارد");
                    langId = language.Id;
                }
                else langId = haslanguage.Id;
                var congressLanguage = new WebDesignLanguage { WebId = websiteId, LanguageId = langId };
                if (!new WebDesignLanguageBO().Insert(this.ConnectionHandler, congressLanguage))
                    throw new Exception("خطایی در ذخیره زبان وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
              
                return true;
            }
            catch (KnownException knownException)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                throw new KnownException(ex.Message, ex);
            }
        }
    }
}
