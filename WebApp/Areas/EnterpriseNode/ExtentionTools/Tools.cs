using Radyn.EnterpriseNode.Tools;

namespace Radyn.WebApp.Areas.EnterpriseNode.ExtentionTools
{
    public static class Tools
    {
        public static string EnterpriseNodeTitle(this Radyn.EnterpriseNode.DataStructure.EnterpriseNode enterprise)
        {
            return enterprise.Title();
        }
    }
}