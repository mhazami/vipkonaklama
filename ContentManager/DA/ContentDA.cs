using Radyn.ContentManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;


namespace Radyn.ContentManager.DA
{
    public sealed class ContentDA : DALBase<Content>
    {
        public ContentDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

     
    }
    internal class ContentCommandBuilder
    {
        
    }
}
