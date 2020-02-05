using System.Configuration;
using Radyn.Common;
using Radyn.ContentManager;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.FormGenerator
{
    public abstract class FormGeneratorBaseFacade<T> : BaseFacade<T> where T : class
    {

        protected FormGeneratorBaseFacade()
            : base(new FormGeneratorConnectionHandler(), false)
        {

        }
        protected FormGeneratorBaseFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {

        }
        public IConnectionHandler FileManagerConnection
        {
            get
            {
                var fileManagerConnection = new FileManagerConnectionHandler();
                return fileManagerConnection.Connection.DataSource == this.ConnectionHandler.Connection.DataSource
                     && fileManagerConnection.Connection.Database == this.ConnectionHandler.Connection.Database
                    ? this.ConnectionHandler
                    : fileManagerConnection;
            }
        }


      
        public IConnectionHandler CommonConnection
        {
            get
            {
                var commonConnectionHandler = new CommonConnectionHandler();
                return commonConnectionHandler.Connection.DataSource == this.ConnectionHandler.Connection.DataSource
                     && commonConnectionHandler.Connection.Database == this.ConnectionHandler.Connection.Database
                    ? this.ConnectionHandler
                    : commonConnectionHandler;
            }
        }
        public IConnectionHandler ContentManagerConnection
        {
            get
            {
                var contentManagerConnectionHandler = new ContentManagerConnectionHandler();
                return contentManagerConnectionHandler.Connection.DataSource == this.ConnectionHandler.Connection.DataSource
                     && contentManagerConnectionHandler.Connection.Database == this.ConnectionHandler.Connection.Database
                    ? this.ConnectionHandler
                    : contentManagerConnectionHandler;
            }
        }
    }
    public class FormGeneratorConnectionHandler : ConnectionHandler
    {
        public FormGeneratorConnectionHandler()
        {
            base.ConnectionString = ConfigurationManager.ConnectionStrings["WebDesignEntities"].ConnectionString; 
        }
    }




}
