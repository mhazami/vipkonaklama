using System;
using System.Data;
using Radyn.ContentManager.BO;
using Radyn.ContentManager.DataStructure;
using Radyn.ContentManager.Facade.Interface;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.ContentManager.Facade
{
    internal sealed class MenuHtmlFacade : ContentManagerBaseFacade<MenuHtml>, IMenuHtmlFacade
    {
        internal MenuHtmlFacade()
        {
        }

        internal MenuHtmlFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

       

        public override bool Insert(MenuHtml MenuHtml)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                
                var MenuHtmlBO = new MenuHtmlBO();
                if (MenuHtml.Enabled)
                {
                    var list = MenuHtmlBO.Where(this.ConnectionHandler, html =>  html.IsExternal == false);
                   foreach (var html in list)
                    {

                        html.Enabled = false;
                        if (!MenuHtmlBO.Update(this.ConnectionHandler, html))
                            throw new Exception("خطایی در ویرایش Html وجود دارد");
                    }
                }
                if (!MenuHtmlBO.Insert(this.ConnectionHandler, MenuHtml))
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

        public override bool Update(MenuHtml MenuHtml)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
               var MenuHtmlBO = new MenuHtmlBO();
                if (MenuHtml.Enabled)
                {
                    var list = MenuHtmlBO.Where(this.ConnectionHandler, html =>  html.IsExternal == false);
                    
                    foreach (var html in list)
                    {

                        html.Enabled = false;
                        if (!MenuHtmlBO.Update(this.ConnectionHandler, html))
                            throw new Exception("خطایی در ویرایش Html وجود دارد");
                    }
                }
                if (!MenuHtmlBO.Update(this.ConnectionHandler, MenuHtml))
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
