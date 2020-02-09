using Radyn.Reservation.Facade;
using Radyn.Reservation.Facade.Interface;

namespace Radyn.Reservation
{
    public class ReservationComponent
    {
        private ReservationComponent()
        {

        }

        private static ReservationComponent _instance;
        public static ReservationComponent Instance
        {
            get { return _instance ?? (_instance = new ReservationComponent()); }
        }

        public IRoomFacade RoomFacade
        {
            get
            {
                return new RoomFacade();
            }
        }

        public ICustomerFacade CustomerFacade
        {
            get
            {
                return new CustomerFacade();
            }
        }

        public IOrderFacade OrderFacade
        {
            get
            {
                return new OrderFacade();
            }
        }

        public IReservePriceFacade ReservePriceFacade
        {
            get
            {
                return new ReservePriceFacade();
            }
        }

        public IRoomTypeFacade RoomTypeFacade
        {
            get
            {
                return new RoomTypeFacade();
            }
        }

        public IReserveTypeFacade ReserveTypeFacade
        {
            get
            {
                return new ReserveTypeFacade();
            }
        }

        public IHotelFacade HotelFacade
        {
            get
            {
                return new HotelFacade();
            }
        }

        public IHotelFloorFacade HotelFloorFacade
        {
            get
            {
                return new HotelFloorFacade();
            }
        }

        public IHotelOfficeFacade HotelOfficeFacade
        {
            get
            {
                return new HotelOfficeFacade();
            }
        }

        public IOfficeRoomFacade OfficeRoomFacade
        {
            get
            {
                return new OfficeRoomFacade();
            }
        }
        public IUserHotelAccessFacade UserHotelAccessFacade
        {
            get
            {
                return new UserHotelAccessFacade();
            }
        }

        public object UserFacade { get; set; }
    }
}
