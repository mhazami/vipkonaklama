using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Reservation.DataStructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Radyn.Reservation.BO
{
    internal class RoomBO : BusinessBase<Room>
    {
        public override bool Insert(IConnectionHandler connectionHandler, Room obj)
        {
            Guid id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            return base.Insert(connectionHandler, obj);
        }

        internal List<dynamic> GetRoomTree(IConnectionHandler connectionHandler, Guid hotelId, List<Guid> checkedList, bool disableselect)
        {
            List<dynamic> tree = new List<dynamic>();
            var floors = new HotelFloorBO().Where(connectionHandler, x => x.HotelId == hotelId);
            var rooms = new RoomBO().Where(connectionHandler, x => x.HotelFloor.HotelId == hotelId);
            foreach (var floor in floors)
            {
                var node = new
                {
                    id = floor.Id,
                    text = floor.Name,
                    state = new { checkbox_disabled = disableselect },
                    parent = "#"

                };
                tree.Add(node);
                foreach (var room in rooms.Where(x => x.FloorId == floor.Id))
                {
                    var node1 = new
                    {
                        id = room.Id,
                        text = room.Title,
                        state = new { checkbox_disabled = disableselect },
                        parent = floor.Id

                    };
                    tree.Add(node1);
                }
            }

            return tree;
        }
    }
}
