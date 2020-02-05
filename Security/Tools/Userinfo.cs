using System;
using Radyn.Security.DataStructure;

namespace Radyn.Security.Tools
{
    public class UserInfo
    {
        public Guid Id { get; set; }
        private User _user;
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                 
            }
        }
        public EnterpriseNode.DataStructure.EnterpriseNode EnterPriseNode { get; set; }
    }

}
