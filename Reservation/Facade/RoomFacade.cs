using System;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Reservation.BO;
using Radyn.Reservation.DataStructure;
using Radyn.Reservation.Facade.Interface;

namespace Radyn.Reservation.Facade
{
    internal sealed class RoomFacade : ReservationBaseFacade<Room>, IRoomFacade
    {
        internal RoomFacade() { }

        internal RoomFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

        public System.Collections.Generic.List<dynamic> GetTree(Guid hotelId, Guid officeId)
        {
            try
            {
                var checkedList = new OfficeRoomBO().Select(this.ConnectionHandler, x => x.RoomId, x => x.OfficeId == officeId);
                return new RoomBO().GetRoomTree(ConnectionHandler, hotelId, checkedList, false);
            }
            catch (KnownException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new KnownException(ex.Message, ex);
            }
        }
    }
}