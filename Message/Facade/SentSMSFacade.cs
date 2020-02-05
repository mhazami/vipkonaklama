using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Message.Facade.Interface;

namespace Radyn.Message.Facade
{
    internal sealed class SentSMSFacade : ISentSMSFacade
    {
        private static RadynMessages _radynMessage;

        internal static RadynMessages RadynMessage
        {

            get { return _radynMessage ?? new RadynMessages(); }
        }

        public bool SendSms(int accountumber, string username, string password, string To, string text)
        {
            try
            {
                return RadynMessage.RadynSendSms(accountumber, username, password, To, text);
            }
            catch (Exception ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public IEnumerable<SMS> Search(int accountumber, string username, string password, string fromdate,
            string todate, string text, byte? status)
        {
            try
            {
                return RadynMessage.RadynSearchSmS(accountumber, username, password, fromdate, todate, text, status);
            }
            catch (Exception ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool ResendSMS(Guid SMSId)
        {
            try
            {
                return RadynMessage.RadynResendSMS(SMSId);
            }
            catch (Exception ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public decimal AccountCredit(int accountumber, string username, string password)
        {
            try
            {
                return RadynMessage.RadynGetAccountCredit(accountumber, username, password);
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public bool SendGroupSms(int accountumber, string username, string password, string[] To, string text)
        {
            try
            {
                return RadynMessage.RadynSendGroupSms(accountumber, username, password, To, text);
            }
            catch (Exception ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }
    }
}
