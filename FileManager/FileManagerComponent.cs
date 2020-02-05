using System.Web.Mvc;
using System.Web.Routing;
using Radyn.FileManager.Facade;
using Radyn.FileManager.Facade.Interface;
using Radyn.FileManager.Handler;
using Radyn.Framework.DbHelper;

namespace Radyn.FileManager
{
    public class FileManagerComponent
    {
        private FileManagerComponent()
        {

        }

        private static FileManagerComponent _instance;
        public static FileManagerComponent Instance
        {
            get { return _instance ?? (_instance = new FileManagerComponent()); }
        }

        private IFileFacade _fileFacade;
        public IFileFacade FileFacade
        {
            get
            {
                return this._fileFacade ?? (this._fileFacade = new FileFacade());
            }
        }
        public IWebDesignFolderFacade WebDesignFolderFacade
        {
            get { return new WebDesignFolderFacade(); }
        }
        public IFileFacade FileTransactionalFacade(IConnectionHandler connectionHandler)
        {
            return  new FileFacade(connectionHandler);
        }
        public IFolderFacade FolderTransactionalFacade(IConnectionHandler connectionHandler)
        {
            return new FolderFacade(connectionHandler);
        }
        private IFolderFacade _folderFacade;
        public IFolderFacade FolderFacade
        {
            get
            {
                return this._folderFacade ?? (this._folderFacade = new FolderFacade());
            }
        }

        public static void RegisterFileManagerRoute()
        {
            RouteTable.Routes.Add("FileManagerdefault",
                new Route(FileManagerContants.FileHandlerRoot.Replace("/", "") + "/{id}",
                    new RouteValueDictionary(new { id = UrlParameter.Optional }),
                    new FileRouteHandler()));
            RouteTable.Routes.Add("FileManager",
                                  new Route(FileManagerContants.FileHandlerRoot.Replace("/", "") + "/{id}/{*cachall}",
                                            new RouteValueDictionary(new { id = UrlParameter.Optional }),
                                            new FileRouteHandler()));
        }
    }
}
