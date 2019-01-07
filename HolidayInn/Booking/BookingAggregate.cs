using Framework.CQRS;
using Events.Booking;
using Events.Cafe;
using System;
using System.Collections;

namespace HolidayInn.Booking
{
    public class BookingAggregate : Aggregate, 
        IHandleCommand<CreateBooking>,
        IHandleCommand<Checkin>,
        IHandleCommand<Checkout>,
        IHandleCommand<CancelBooking>,
        IApplyEvent<BookingCreated>,
        IApplyEvent<CheckinHappened>,
        IApplyEvent<CheckoutHappened>
    {

        private bool bookingcreated = false;
        private bool checkinHappened = false;
        private bool checkoutHappened = false;

        public void Apply(BookingCreated e)
        {
            bookingcreated = true;
        }

        public void Apply(CheckinHappened e)
        {
            checkinHappened = true; ;
        }

        public void Apply(CheckoutHappened e)
        {
            checkoutHappened = true;
        }

        public IEnumerable Handle(CreateBooking c)
        {
            yield return new BookingCreated
            {
                Id = c.Id,
                RoomNumber = c.RoomNumber,
                RoomTarrifPerNight=c.RoomTarrifPerNight,
                Client = c.Client,
                CheckinDate = c.CheckinDate,
                CheckoutDate = c.CheckoutDate,
                StaffId=c.StaffId
            };
        }

        public IEnumerable Handle(Checkin c)
        {
            if (!bookingcreated)
                throw new BookingNotCreated();

            yield return new CheckinHappened
            {
                Id = c.Id,
                ActualCheckinDate = DateTime.Now
            };
        }

        public IEnumerable Handle(Checkout c)
        {
            //if (!checkinHappened)
            //    throw new CheckinNotHappened();

            yield return new CheckoutHappened
            {
                Id = c.Id,
                ActualCheckoutDate = DateTime.Now
            };

        }

        public IEnumerable Handle(CancelBooking c)
        {
            if (!bookingcreated)
                throw new BookingNotCreated();
            if (checkoutHappened)
                throw new CannotCancelAfterCheckingOut();         

            yield return new BookingCancelled
            {
                Id = c.Id              

            };
        }
    }
}
