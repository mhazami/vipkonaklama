using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using Radyn.FAQ.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.FAQ.BO;
using Radyn.FAQ.Facade.Interface;

namespace Radyn.FAQ.Facade
{
    internal sealed class WebDesignFAQFacade : FAQBaseFacade<DataStructure.WebDesignFAQ>, IWebDesignFAQFacade
    {
        internal WebDesignFAQFacade() { }

        internal WebDesignFAQFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

       
        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                var congressFaqbo = new WebDesignFAQBO();
                var obj = congressFaqbo.Get(this.ConnectionHandler, keys);
                if (!congressFaqbo.Delete(this.ConnectionHandler, keys))
                    throw new Exception("خطایی در حذف FAQ وجود دارد");
                if (!new FAQBO().Delete(ConnectionHandler,obj.FAQId))
                    throw new Exception("خطایی در حذف FAQ وجود دارد");
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
        public IEnumerable<FAQ.DataStructure.FAQ> GetByWebSiteId(Guid websiteId)
        {
            try
            {

                var list = new List<FAQ.DataStructure.FAQ>();
                var faqFacade = FAQComponent.Instance.FAQFacade;
                var byFilter = new WebDesignFAQBO().Where(this.ConnectionHandler, html => html.WebId == websiteId);
                foreach (var congressHtml in byFilter)
                {
                    var html = faqFacade.Get(congressHtml.FAQId);
                    if (html == null) continue;
                    list.Add(html);
                }
                return list;
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

        public IEnumerable<FAQ.DataStructure.FAQ> Search(Guid websiteId, string value)
        {
            try
            {

                var list = new List<FAQ.DataStructure.FAQ>();
                var faqFacade = FAQComponent.Instance.FAQFacade.Search(value);
                var congressFaqbo = new WebDesignFAQBO();
                foreach (var congressHtml in faqFacade)
                {
                    var html = congressFaqbo.Get(this.ConnectionHandler, websiteId, congressHtml.Id);
                    if (html == null) continue;
                    list.Add(congressHtml);
                }
                return list;
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

        public bool Insert(Guid websiteId, FAQ.DataStructure.FAQ faq, FAQContent faqContent, HttpPostedFileBase image)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                faq.IsExternal = true;
                if (!new FAQBO().Insert(ConnectionHandler, FileManagerConnection,faq, faqContent, image))
                    throw new Exception("خطایی در ذخیره FAQ وجود دارد");
                var congressFaq = new DataStructure.WebDesignFAQ { FAQId = faq.Id, WebId = websiteId };
                if (!new WebDesignFAQBO().Insert(this.ConnectionHandler, congressFaq))
                    throw new Exception("خطایی در ذخیره FAQ وجود دارد");
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
