using System;
using System.Collections.Generic;

namespace Radyn.Message.Facade.Interface
{
    public interface ISentSMSFacade
    {
        bool SendSms(int accountumber, string username, string password, string To, string text);
        bool SendGroupSms(int accountumber, string username, string password, string[] To, string text);

        IEnumerable<SMS> Search(int accountumber, string username, string password, string fromdate, string todate,
            string text, byte? status);

        decimal AccountCredit(int accountumber, string username, string password);
        bool ResendSMS(Guid SMSId);
    }
}
