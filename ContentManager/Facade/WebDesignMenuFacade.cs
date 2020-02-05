using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.ContentManager.BO;
using Radyn.ContentManager.DataStructure;
using Radyn.ContentManager.Facade.Interface;

namespace Radyn.ContentManager.Facade
{
    internal sealed class WebDesignMenuFacade : ContentManagerBaseFacade<WebDesignMenu>, IWebDesignMenuFacade
    {
        internal WebDesignMenuFacade() { }

        internal WebDesignMenuFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }



        public bool Insert(Guid websiteId, ContentManager.DataStructure.Menu menu,  HttpPostedFileBase file)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                menu.IsExternal = true;
                if (menu.Order == 0)
                {
                    var menus = new WebDesignMenuBO().Max(this.ConnectionHandler,x=>x.WebSiteMenu.Order, x => x.WebId == websiteId);
                    menu.Order = menus == 0 ? 1 : menus + 1;
                }
                if (!new MenuBO().Insert(ConnectionHandler, FileManagerConnection, menu,file))
                    throw new Exception("خطایی در ذخیره منو وجود دارد");
                var congressMenu = new WebDesignMenu { MenuId = menu.Id, WebId = websiteId };
                if (!new WebDesignMenuBO().Insert(this.ConnectionHandler, congressMenu))
                    throw new Exception("خطایی در ذخیره منو وجود دارد");
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




        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
               var congressMenuBo = new WebDesignMenuBO();
                var obj = congressMenuBo.Get(this.ConnectionHandler, keys);
                if (!congressMenuBo.Delete(this.ConnectionHandler, keys))
                    throw new Exception("خطایی در حذف منو وجود دارد");
                if (!new MenuBO().Delete(ConnectionHandler, FileManagerConnection,obj.MenuId))
                    throw new Exception("خطایی در حذف منو وجود دارد");
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
        public List<Radyn.ContentManager.DataStructure.Menu> MenuTree(Guid websiteId, string culture)
        {
            try
            {
                var menuBo = new WebDesignMenuBO();
                return menuBo.GetParentsAdnChilds(this.ConnectionHandler, websiteId, culture);

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
        public IEnumerable<ContentManager.DataStructure.Menu> MenuTree(Guid websiteId)
        {
            try
            {
                var menuBo = new WebDesignMenuBO();
                var list = menuBo.GetParents(this.ConnectionHandler, websiteId);
                var Id = menuBo.Select(ConnectionHandler, x => x.MenuId, x => x.WebId == websiteId);
                foreach (var variable in list)
                {
                    var child = ContentManagerComponent.Instance.MenuFacade.GetChildMenu(variable.Id, Guid.Empty);
                    foreach (var menu in child)
                    {
                        if (Id.Any(x => x.Equals(menu.Id)) && menu.Enabled)
                            variable.Children.Add(menu);
                    }

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
    }
}
