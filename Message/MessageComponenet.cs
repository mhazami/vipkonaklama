using Radyn.Message.Facade;
using Radyn.Message.Facade.Interface;

namespace Radyn.Message
{
   public class MessageComponenet
    {
       internal MessageComponenet()
       {
           
       }

       private static MessageComponenet _instance;
       public static MessageComponenet Instance
       {
           get { return _instance ?? (_instance = new MessageComponenet()); }
       }
       private static InternalMessageComponenet _sentInternalMessageinstance;
       public static InternalMessageComponenet SentInternalMessageInstance
       {
           get { return _sentInternalMessageinstance ?? (_sentInternalMessageinstance = new InternalMessageComponenet()); }
       }
       private ISentMailFacade _sentMailFacade;
       public ISentMailFacade MailFacade
       {
           get { return _sentMailFacade??new SentMailFacade(); }
       }

       private ISentSMSFacade _sentSMSFacade;
       public ISentSMSFacade SMSFacade
       {
           get { return _sentSMSFacade??new SentSMSFacade(); }
       }
     
      
    }
}
