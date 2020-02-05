using Radyn.Framework.DbHelper;
using Radyn.Security.Facade;
using Radyn.Security.Facade.Interface;

namespace Radyn.Security
{
    public class SecurityComponent
    {

        private SecurityComponent()
        {

        }

        private static SecurityComponent _instance;
        public static SecurityComponent Instance
        {
            get { return _instance ?? (_instance = new SecurityComponent()); }
        }

        public IOperationFacade OperationFacade
        {
            get
            {
                return new OperationFacade();
            }
        }

        public ITrackerFacade TrackerFacade
        {
            get
            {
                return new TrackerFacade();
            }
        }

        public IMenuFacade MenuFacade
        {
            get { return new MenuFacade(); }
        }

        public IMenuGroupFacade MenuGroupFacade
        {
            get { return new MenuGroupFacade(); }
        }

        public IActionFacade ActionFacade
        {
            get { return new ActionFacade(); }
        }

        public IRoleFacade RoleFacade
        {
            get { return new RoleFacade(); }
        }
    
        public IGroupFacade GroupFacade
        {
            get { return new GroupFacade(); }
        }
        public IRoleActionFacade RoleActionFacade
        {
            get { return new RoleActionFacade(); }
        }
        public IUserFacade UserFacade
        {
            get { return new UserFacade(); }
        }
        public IUserFacade UserTransactionalFacade(IConnectionHandler connectionHandler)
        {
             return new UserFacade(connectionHandler); 
        }
        public IUserRoleFacade UserRoleFacade
        {
            get { return new UserRoleFacade(); }
        }
        public IOperationMenuFacade OperationMenuFacade
        {
            get { return new OperationMenuFacade(); }
        }
        public IUserOperationFacade UserOperationFacade
        {
            get { return new UserOperationFacade(); }
        }
        public IUserOperationFacade UserOperationTransactionalFacade(IConnectionHandler connectionHandler)
        {
             return new UserOperationFacade(connectionHandler); 
        }
        public IUserGroupFacade UserGroupFacade
        {
            get { return new UserGroupFacade(); }
        }
        public IUserActionFacade UserActionFacade
        {
            get { return new UserActionFacade(); }
        }
        public IUserMenuFacade UserMenuFacade
        {
            get { return new UserMenuFacade(); }
        }
        public IRoleMenuFacade RoleMenuFacade
        {
            get { return new RoleMenuFacade(); }
        }
        public IRoleOperationFacade RoleOperationFacade
        {
            get { return new RoleOperationFacade(); }
        }
        public IGroupRoleFacade GroupRoleFacade
        {
            get { return new GroupRoleFacade(); }
        }


    }
}
