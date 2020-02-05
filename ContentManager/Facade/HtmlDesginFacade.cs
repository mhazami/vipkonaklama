using System;
using System.Collections.Generic;
using System.Data;
using Radyn.ContentManager.BO;
using Radyn.ContentManager.DataStructure;
using Radyn.ContentManager.Facade.Interface;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.ContentManager.Facade
{
    internal sealed class HtmlDesginFacade : ContentManagerBaseFacade<HtmlDesgin>, IHtmlDesginFacade
    {
        internal HtmlDesginFacade()
        {
        }

        internal HtmlDesginFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

      
        public Dictionary<string, string> ReturnCustomeAttributes(Guid htmdesignId)
        {
            try
            {
                return new HtmlDesginBO().ReturnCustomeAttributes(this.ConnectionHandler, htmdesignId);
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



        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
              var htmlDesginBo = new HtmlDesginBO();
               if (!htmlDesginBo.Delete(this.ConnectionHandler, keys))
                    throw new Exception("خطایی در حذف  HTML وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
               
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public override bool Insert(HtmlDesgin htmlDesgin)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                
                var htmlDesginBO = new HtmlDesginBO();
               if (!htmlDesginBO.Insert(this.ConnectionHandler, htmlDesgin))
                    throw new Exception("خطایی در ذخیره HTML وجود دارد");

              
                this.ConnectionHandler.CommitTransaction();
               
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
              Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public override bool Update(HtmlDesgin htmlDesgin)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
               var htmlDesginBO = new HtmlDesginBO();
               if (!htmlDesginBO.Update(this.ConnectionHandler, htmlDesgin))
                    throw new Exception("خطایی در ویرایش HTML وجود دارد");
               
                this.ConnectionHandler.CommitTransaction();
               
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
               Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
              Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

       
    }
}
