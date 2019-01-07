using Events.Cafe;
using System;

namespace Events.Booking
{
    public class BookingCreated
    {
        public Guid Id;
        public int RoomNumber;
        public decimal RoomTarrifPerNight;
        public Customer Client;
        public DateTime CheckinDate;
        public DateTime CheckoutDate;
        public string StaffId;
       

    }
}
