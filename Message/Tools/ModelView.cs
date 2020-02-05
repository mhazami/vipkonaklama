using System;

namespace Radyn.Message.Tools
{
   public class ModelView
    {
       public class MessageModel
       {
           public string EmailTitle { get; set; }
           public string EmailBody { get; set; }
           public string SMSBody { get; set; }
           public bool SendEmail { get; set; }
           public bool SendSMS { get; set; }
           public bool SendIntrenalMessage { get; set; }
           public string Email { get; set; }
           public string Mobile { get; set; }
           public string Id { get; set; }
           public string Date { get; set; }
       }
       
       public class InternalMailSelectUser
       {
           public string Title { get; set; }
           public Guid Id { get; set; }
           public Guid? PhotoId { get; set; }
           public bool IsSelcted { get; set; }

       }
       
   }
}
