using Radyn.FileManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;


namespace Radyn.FileManager.DAL
{
    public sealed class WebDesignFolderDA : DALBase<WebDesignFolder>
    {
        public WebDesignFolderDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        { }
    }
    internal class WebDesignFolderCommandBuilder
    {
    }
}
