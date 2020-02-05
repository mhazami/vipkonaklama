using Radyn.ContentManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.ContentManager.BO
{
    internal class ContentContentBO : BusinessBase<ContentContent>
    {
        protected override void CheckConstraint(IConnectionHandler connectionHandler, ContentContent item)
        {
            base.CheckConstraint(connectionHandler, item);

            /*
            if (item.Abstract != null)
            {
                if (item.Abstract.Contains("'"))
                    item.Abstract = item.Abstract.Replace("'", "''");
            }
            if (item.Text != null)
            {
                if (item.Text.Contains("'"))
                {
                    item.Text = item.Text.Replace("'", "''");
                }
            }
            */
          

        }
    }
}
