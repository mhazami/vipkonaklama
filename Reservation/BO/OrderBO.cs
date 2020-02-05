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
            if (item.RoomId != null)
            {
                RoomBO roomBo = new RoomBO();
                Room room = roomBo.Get(connectionHandler, item.RoomId);
                room.Idle = false;
                roomBo.Update(connectionHandler, room);
            }
            SetTotalPrice(connectionHandler, item);
        }

        internal void SetTotalPrice(IConnectionHandler connectionHandler, Order item)
        {
            item.TotalPrice = GetTotalPrice(connectionHandler, item.EntryDate, item.ExitDate, item.RoomTypeId, item.ReserveType);
        }

        internal decimal GetTotalPrice(IConnectionHandler connectionHandler, DateTime startdate, DateTime enddate, byte roomtypeId, Enum.ReserveType reserveType)
        {

            Tools.Utility tools = new Tools.Utility();
            int days = tools.GetDaysCount(startdate, enddate);
            if (!startdate.Equals(enddate))
            {
                reserveType = Enum.ReserveType.Daily;
            }

            int weekends = tools.GetWeekendCount(startdate, enddate);
            PredicateBuilder<ReservePrice> query = new PredicateBuilder<ReservePrice>();
            query.And(x => x.RoomTypeId == roomtypeId);
            query.And(x => x.ReserveType == reserveType);
            List<ReservePrice> list = new ReservePriceBO().Where(connectionHandler, query.GetExpression());

            int normaldays = days == weekends ? 0 : days - weekends;
            decimal normalPrice = list != null ? list.FirstOrDefault(x => x.DayType == Enum.DayType.Normal).PerDayPrice : 0;
            decimal weekendPrice = list != null ? list.FirstOrDefault(x => x.DayType == Enum.DayType.Weekend).PerDayPrice : 0;
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
            if (!base.Insert(connectionHandler, order))
            {
                throw new Exception("Rezervasyon hatası");
            }
            return true;
        }

        internal bool UpdateWithCustomer(IConnectionHandler connectionHandler, Order order)
        {
            //todo:translate
            if (!new CustomerBO().Insert(connectionHandler, order.Customer))
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
