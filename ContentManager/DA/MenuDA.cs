using Radyn.ContentManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;


namespace Radyn.ContentManager.DA
{
    public sealed class MenuDA : DALBase<Menu>
    {
        public MenuDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

        


        public int GetMaxOrder()
        {

            var menuCommandBuilder = new MenuCommandBuilder();
            var query = menuCommandBuilder.GetMaxOrder();
            return DBManager.ExecuteScalar<int>(base.ConnectionHandler, query);
        }

    }
    internal class MenuCommandBuilder
    {
        
       
        public string GetMaxOrder()
        {
            return string.Format("SELECT Max([ORDER]) FROM [ContentManage].[Menu] WHERE  IsExternal=0  ");
        }

      
    }
}
