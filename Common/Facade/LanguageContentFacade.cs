using System;
using System.Collections.Generic;
using System.Configuration;
using Radyn.Common.BO;
using Radyn.Common.Component;
using Radyn.Common.Facade.Interface;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Common.Facade
{
    internal sealed class LanguageContentFacade : CommonBase<LanguageContent>, ILanguageContentFacade
    {
        internal LanguageContentFacade()
        {
        }

        internal LanguageContentFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }
      

      
        public List<LanguageContent> GetByMoudelname(string moudelname,string culture)
        {
            try
            {
                return new LanguageContentBO().GetByMoudelname(this.ConnectionHandler, moudelname, culture);
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
        public override bool Insert(LanguageContent obj)
        {
            try
            {
                var languageSetting = (LanguageSetting)ConfigurationManager.GetSection("radyn/language");
                if (languageSetting != null)
                    obj.IsDefault = languageSetting.DefaultCulture.Equals(obj.LanguageId);
                return new LanguageContentBO().Insert(this.ConnectionHandler, obj);
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

       

    

       
       
       
        public LanguageContent GetReource(string key, string culture)
        {
            
            var item = base.Get(key, culture);
            if (item == null)
             {
                var languageContent= new LanguageContent
                {
                    Key = key,
                    LanguageId = culture,
                    
                };
                base.Insert(languageContent);
            }
            return item;
        }
        
       
    }
}
