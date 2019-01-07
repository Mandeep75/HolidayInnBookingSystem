using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Events.Cafe;
using Framework.CQRS;
using Events.Booking;

namespace CafeReadModels
{
    public class OpenBookings : IOpenBookingQueries,
        ISubscribeTo<BookingCreated>,        
        ISubscribeTo<BookingCancelled>,
        ISubscribeTo<CheckoutHappened>
    {
             

        public class BookingInvoice
        {
            public Guid BookingId;
            public int RoomNumber;            
            public decimal Total;
            public bool HasPendingCheckout;
        }

        public class BookingStatus
        {
            public Guid BookingId;
            public int RoomNumber;
            public decimal RoomTarrifPerNight;
            public Customer Client;
            public DateTime CheckinDate;
            public DateTime CheckoutDate;
            public bool HasClientCheckedOut;
        }

        private class Booking
        {
            public int RoomNumber;
            public decimal RoomTarrifPerNight;
            public Customer Client;
            public DateTime CheckinDate;
            public DateTime CheckoutDate;            
            public string StaffId;
            public bool HasClientCheckedOut;
        }

        private Dictionary<Guid, Booking> todoByBooking =
            new Dictionary<Guid, Booking>();

        public List<int> BookedRoomNumbers()
        {
            lock (todoByBooking)
                return (from booking in todoByBooking
                        select booking.Value.RoomNumber
                       ).OrderBy(i => i).ToList();
        }



        public BookingStatus BookingForRoomNumber(int roomNumber)
        {
            lock (todoByBooking)
                return (from booking in todoByBooking
                        where booking.Value.RoomNumber == roomNumber
                        select new BookingStatus
                        {
                            BookingId = booking.Key,
                            RoomNumber=booking.Value.RoomNumber,
                            RoomTarrifPerNight=booking.Value.RoomTarrifPerNight,
                            CheckinDate=booking.Value.CheckinDate,
                            CheckoutDate=booking.Value.CheckoutDate,
                            Client=booking.Value.Client,
                            HasClientCheckedOut=booking.Value.HasClientCheckedOut
                        }
                       ).First();
        }

        public List<BookingStatus> GetAllBookingsList()
        {
             lock (todoByBooking)
            return (from booking in todoByBooking                        
                        select new BookingStatus
                        {
                            BookingId = booking.Key,
                            RoomNumber=booking.Value.RoomNumber,
                            RoomTarrifPerNight=booking.Value.RoomTarrifPerNight,
                            CheckinDate=booking.Value.CheckinDate,
                            CheckoutDate=booking.Value.CheckoutDate,
                            Client=booking.Value.Client,
                            HasClientCheckedOut=booking.Value.HasClientCheckedOut
    }
                       ).ToList();
}

        public Guid BookingIdForRoomNumber(int roomNumber)
        {
            lock (todoByBooking)
                return (from booking in todoByBooking
                        where booking.Value.RoomNumber == roomNumber
                        select booking.Key
                       ).First();
        }


        public BookingInvoice InvoiceForRoom(int roomNumber)
        {
            KeyValuePair<Guid, Booking> booking;
            lock (todoByBooking)
                booking = todoByBooking.First(t => t.Value.RoomNumber == roomNumber);

            lock (booking.Value)
                return new BookingInvoice
                {
                    BookingId = booking.Key,
                    RoomNumber = booking.Value.RoomNumber,
                   
                    Total = ( booking.Value.CheckoutDate- booking.Value.CheckinDate).Days * booking.Value.RoomTarrifPerNight
                    
                };
        }

        

        public void Handle(BookingCreated e)
        {
            lock (todoByBooking)
                todoByBooking.Add(e.Id, new Booking
                {
                   RoomNumber=e.RoomNumber,
                   RoomTarrifPerNight=e.RoomTarrifPerNight,
                   CheckinDate=e.CheckinDate,
                   CheckoutDate=e.CheckoutDate,
                   Client=e.Client,
                   StaffId=e.StaffId
                });
        }

        

        public void Handle(BookingCancelled e)
        {
            lock (todoByBooking)
                todoByBooking.Remove(e.Id);
        }

        private Booking getBooking(Guid id)
        {
            lock (todoByBooking)
                return todoByBooking[id];
        }

        public void Handle(CheckoutHappened e)
        {
            lock (todoByBooking)
            {
                 todoByBooking[e.Id].HasClientCheckedOut=true;
            }
        }
    }
}
