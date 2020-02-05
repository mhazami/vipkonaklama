using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Message.DataStructure;
using Radyn.Message.Tools;

namespace Radyn.Message.Facade.Interface
{
public interface IMailInfoFacade : IBaseFacade<MailInfo>
{
   
    

   MailInfo GetMail(Guid userId, Guid id);
   IEnumerable<ModelView.InternalMailSelectUser> SearchUsers(Guid currentUserId, string selected, string value);

    bool DeleteGroup(string id);
}
}
