using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Reservation.DataStructure;
using Radyn.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Radyn.Reservation.BO
{
    internal class OrderBO : BusinessBase<Order>
    {
        public override bool Insert(IConnectionHandler connectionHandler, Order obj)
        {
            Guid id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            obj.OrderDate = DateTime.Now;
            SetOrderId(connectionHandler, obj);
            return base.Insert(connectionHandler, obj);
        }

        protected override void CheckConstraint(IConnectionHandler connectionHandler, Order item)
        {
            base.CheckConstraint(connectionHandler, item);
            RoomBO roomBo = new RoomBO();
            Order oldOrder = Get(connectionHandler, item.Id);

            if (oldOrder != null && oldOrder.RoomId.HasValue && oldOrder.RoomId.Value != item.RoomId)
            {
                Room oldRoom = roomBo.Get(connectionHandler, oldOrder.RoomId);
                oldRoom.Idle = true;
                roomBo.Update(connectionHandler, oldRoom);
            }

            if (item.RoomId != null)
            {
                Room room = roomBo.Get(connectionHandler, item.RoomId);
                room.Idle = false;
                roomBo.Update(connectionHandler, room);
            }
           
        }



        internal decimal GetTotalPrice(IConnectionHandler connectionHandler, DateTime startdate, DateTime enddate, byte roomtypeId, Guid reserveType)
        {

            Tools.Utility tools = new Tools.Utility();
            int days = tools.GetDaysCount(startdate, enddate);
            if (!startdate.Equals(enddate))
            {
                //reserveType = Enum.ReserveType.Daily;
            }

            int weekends = tools.GetWeekendCount(startdate, enddate);
            PredicateBuilder<ReservePrice> query = new PredicateBuilder<ReservePrice>();
            query.And(x => x.RoomTypeId == roomtypeId);
            //query.And(x => x.ReserveType == reserveType);
            List<ReservePrice> list = new ReservePriceBO().Where(connectionHandler, query.GetExpression());

            int normaldays = days == weekends ? 0 : days - weekends;
            if (startdate.Date.Equals(enddate.Date))
            {
                normaldays = 1;
            }
            decimal normalPrice = list.Count() != 0 ? list.FirstOrDefault(x => x.DayType == Enum.DayType.Normal).PerDayPrice : 0;
            decimal weekendPrice = list.Count() != 0 ? list.FirstOrDefault(x => x.DayType == Enum.DayType.Weekend).PerDayPrice : 0;
            decimal totalPrice = (normaldays * normalPrice) + (weekends * weekendPrice);

            return totalPrice;
        }

        internal bool InsertWithCustomer(IConnectionHandler connectionHandler, Order order)
        {
            if (!new CustomerBO().Insert(connectionHandler, order.Customer))
            {
                throw new Exception("Kişisel bilgiler kaydedilirken hata oluştu");
            }
            order.CustomerId = order.Customer.Id;
            if (!Insert(connectionHandler, order))
            {
                throw new Exception("Rezervasyon hatası");
            }
            return true;
        }

        internal bool UpdateWithCustomer(IConnectionHandler connectionHandler, Order order)
        {
            //todo:translate
            if (order.CustomerId.HasValue)
            {
                Customer customer = new CustomerBO().Get(connectionHandler, order.CustomerId);
                if (customer != null)
                {
                    if (!new CustomerBO().Update(connectionHandler, order.Customer))
                    {
                        throw new Exception("Kişisel bilgiler kaydedilirken hata oluştu");
                    }
                }
                else if (!new CustomerBO().Insert(connectionHandler, order.Customer))
                {
                    throw new Exception("Kişisel bilgiler kaydedilirken hata oluştu");
                }
            }
            else if (!new CustomerBO().Insert(connectionHandler, order.Customer))
            {
                throw new Exception("Kişisel bilgiler kaydedilirken hata oluştu");
            }

            order.CustomerId = order.Customer.Id;
            if (!base.Update(connectionHandler, order))
            {
                throw new Exception("Rezervasyon hatası");
            }
            return true;
        }

        private void SetOrderId(IConnectionHandler connectionHandler, Order order)
        {
            Random rnd = new Random(100000);
            bool isOk = false;
            int id = 0;
            while (!isOk)
            {
                id = rnd.Next();
                if (id < 99999999 && !Where(connectionHandler, x => x.OrderId == id).Any())
                {
                    isOk = true;
                }
            }
            order.OrderId = id;
        }
    }
}
