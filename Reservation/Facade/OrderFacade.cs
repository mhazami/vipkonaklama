using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Reservation;
using Radyn.Reservation.BO;
using Radyn.Reservation.DataStructure;
using Radyn.Reservation.Facade.Interface;
using System;
using System.Data;

namespace Radyn.News.Facade
{
    internal sealed class OrderFacade : ReservationBaseFacade<Order>, IOrderFacade
    {
        internal OrderFacade() { }

        internal OrderFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

        public decimal GetTotalPrice(DateTime startdate, DateTime enddate, byte roomtypeId, Reservation.Enum.ReserveType reserveType)
        {
            return new OrderBO().GetTotalPrice(this.ConnectionHandler, startdate, enddate, roomtypeId, reserveType);
        }

        public bool InsertWithCustomer(Order order)
        {
            try
            {
                ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                if (new OrderBO().InsertWithCustomer(ConnectionHandler, order))
                {
                    ConnectionHandler.CommitTransaction();
                }
                return true;
            }
            catch (KnownException ex)
            {
                ConnectionHandler.RollBack();
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                ConnectionHandler.RollBack();
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool UpdateWithCustomer(Order order)
        {
            try
            {
                ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                if (new OrderBO().UpdateWithCustomer(ConnectionHandler, order))
                {
                    ConnectionHandler.CommitTransaction();
                }
                return true;
            }
            catch (KnownException ex)
            {
                ConnectionHandler.RollBack();
                //Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                ConnectionHandler.RollBack();
                //Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }
    }
}