using System.Configuration;
using Radyn.Common;
using Radyn.ContentManager;
using Radyn.EnterpriseNode;
using Radyn.FileManager;
using Radyn.FormGenerator;
using Radyn.Framework;
using Radyn.Framework.DbHelper;


namespace Radyn.WebDesign
{

    public abstract class WebDesignBaseFacade<T> : BaseFacade<T> where T : class
    {
        protected WebDesignBaseFacade()
            : base(new SecurityConnectionHandler(), false)
        {

        }

        protected WebDesignBaseFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {

        }
        
        public IConnectionHandler EnterpriseNodeConnection
        {
            get
            {
                var enterpriseNodeConnection = new EnterpriseNodeConnectionHandler();
                return enterpriseNodeConnection.Connection.DataSource == this.ConnectionHandler.Connection.DataSource
                       && enterpriseNodeConnection.Connection.Database == this.ConnectionHandler.Connection.Database
                    ? this.ConnectionHandler
                    : enterpriseNodeConnection;
            }
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
      
       
        public IConnectionHandler ContentManagerConnection
        {
            get
            {
                var contentManagerConnection = new ContentManagerConnectionHandler();
                return contentManagerConnection.Connection.DataSource == this.ConnectionHandler.Connection.DataSource
                     && contentManagerConnection.Connection.Database == this.ConnectionHandler.Connection.Database
                    ? this.ConnectionHandler
                    : contentManagerConnection;
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
       
        public IConnectionHandler FormGeneratorConnection
        {
            get
            {
                var formGeneratorrConnection = new FormGeneratorConnectionHandler();
                return formGeneratorrConnection.Connection.DataSource == this.ConnectionHandler.Connection.DataSource
                     && formGeneratorrConnection.Connection.Database == this.ConnectionHandler.Connection.Database
                    ? this.ConnectionHandler
                    : formGeneratorrConnection;
            }
        }

       
        
       
    }


    public class SecurityConnectionHandler : ConnectionHandler
    {

        public SecurityConnectionHandler()
        {
           base.ConnectionString = ConfigurationManager.ConnectionStrings["WebDesignEntities"].ConnectionString;
        }

    }


}
