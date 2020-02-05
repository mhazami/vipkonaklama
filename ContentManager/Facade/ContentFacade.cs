using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Radyn.ContentManager.BO;
using Radyn.ContentManager.DataStructure;
using Radyn.ContentManager.Facade.Interface;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.ContentManager.Facade
{
    internal sealed class ContentFacade : ContentManagerBaseFacade<Content>, IContentFacade
    {
        internal ContentFacade()
        {
        }

        internal ContentFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }




        public IEnumerable<Content> Search(string qry)
        {
            try
            {
                var list = new ContentBO().Search(this.ConnectionHandler, qry);
                return list;
            }
            catch (KnownException ex)
            {
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }

        }

        public override bool Insert(Content obj)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new ContentBO().Insert(this.ConnectionHandler, obj))
                    throw new Exception("خطایی در ذخیره محتوا وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
               if (!new ContentBO().Delete(this.ConnectionHandler, keys))
                    throw new Exception("خطایی در حذف  محتوا وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }



        public bool Update(Content content, ContentContent contentContent)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new ContentBO().Update(this.ConnectionHandler, content,contentContent))
                    throw new Exception("خطایی در ویرایش محتوا وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }

        }

        public bool Insert(Content content, ContentContent contentContent)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new ContentBO().Insert(this.ConnectionHandler, content,contentContent))
                    throw new Exception("خطایی در ذخیره محتوا وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }



        public string GetHtml(int Id, string culture)
        {
            try
            {
                var content = new ContentBO().Get(this.ConnectionHandler, Id);
                if (content == null) return string.Empty;
                return new ContentBO().GetHtml(this.ConnectionHandler, content, culture);
            }
            catch (KnownException ex)
            {

                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }
       

        public string GetScript(int Id)
        {
            try
            {
                var content = new ContentBO().Get(this.ConnectionHandler, Id);
                if (content == null) return string.Empty;
                var st = new StringBuilder();
                st.Append(content.UserScript);
                return st.ToString();
            }
            catch (KnownException ex)
            {
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

