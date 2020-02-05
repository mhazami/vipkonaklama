using System.Collections.Generic;
using System.Web;
using Radyn.Framework;
using Radyn.Message.DataStructure;

namespace Radyn.Message.Facade.Interface
{
    public interface ISentMailFacade : IBaseFacade<SentMail>
    {


        bool SendMail(string To, string subject = "", string Body = "", string Cc = "", string Bcc = "", List<HttpPostedFileBase> Attachments = null, bool Persist = false);

        bool SendGroupMail(string[] To, string subject, string Body, string[] Cc = null, string[] Bcc = null, List<HttpPostedFileBase> Attachments = null, bool Persist = false);

        bool SendGroupMailWithInterval(string[] To, string subject, string Body, string[] Cc = null, string[] Bcc = null, List<HttpPostedFileBase> Attachments = null, bool Persist = false, int intervalSecond = 20);


        bool SendMail(string mailHost,string mailPassword,string mailUserName,string mailFrom,short mailPort,string mailSenderDisplayName,bool enableSsl,string To, string subject = "", string Body = "", string Cc = "", string Bcc = "", List<HttpPostedFileBase> Attachments = null, bool Persist = false);

        bool SendGroupMail(string mailHost, string mailPassword, string mailUserName, string mailFrom, short mailPort, string mailSenderDisplayName, bool enableSsl, string[] To, string subject, string Body, string[] Cc = null, string[] Bcc = null, List<HttpPostedFileBase> Attachments = null, bool Persist = false);

        bool SendGroupMailWithInterval(string mailHost, string mailPassword, string mailUserName, string mailFrom, short mailPort, string mailSenderDisplayName, bool enableSsl, string[] To, string subject, string Body, string[] Cc = null, string[] Bcc = null, List<HttpPostedFileBase> Attachments = null, bool Persist = false, int intervalSecond = 20);
        
        
      
    
    
    
    }
}
