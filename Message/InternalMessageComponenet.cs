
using Radyn.Framework.DbHelper;
using Radyn.Message.Facade;
using Radyn.Message.Facade.Interface;

namespace Radyn.Message
{
   public class InternalMessageComponenet
    {
       internal InternalMessageComponenet()
       {
           
       }
       private static InternalMessageComponenet _instance;
       public static InternalMessageComponenet Instance
       {
           get { return _instance ?? (_instance = new InternalMessageComponenet()); }
       }

       private IMailAttachmentFacade _mailAttachmentFacade;
       public IMailAttachmentFacade MailAttachmentFacade
       {
           get { return _mailAttachmentFacade ?? new MailAttachmentFacade(); }
       }

       private IMailBoxFacade _mailBoxFacade;
       public IMailBoxFacade MailBoxFacade
       {
           get { return _mailBoxFacade ?? new MailBoxFacade(); }
       }
       public IMailBoxFacade MailBoxTransactionalFacade(IConnectionHandler connectionHandler)
       {
          return new MailBoxFacade(connectionHandler); 
       }
       private IMailInfoFacade _mailInfoFacade;
       public IMailInfoFacade MailInfoFacade
       {
           get { return _mailInfoFacade ?? new MailInfoFacade(); }
       }

    }
}
