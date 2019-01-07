using HolidayInn.Booking;
using Events.Booking;
using Framework.CQRS;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BookingTests
{
    [TestClass]
    public class CreateBookingTests : BDDTest<BookingAggregate>
    {

        private Guid testId;     


        private int roomNumber;
        private decimal roomTarrif;
        private DateTime checkinDate;
        private DateTime checkoutDate;
        private Customer client;
        private string staffId;


        [TestInitialize]
        public void Initialize()
        {
            testId = Guid.NewGuid();
            roomNumber = 2;
            roomTarrif = 100;
            checkinDate = DateTime.Now.AddDays(10);
            checkoutDate = DateTime.Now.AddDays(13);
            client = new Customer { Name = "Amit", Phone = "088556", Email = "abc@yahoo.com" };
            staffId = "Harry0123";


        }

        [TestMethod]
        public void CanCreateANewBooking()
        {
            Test(
                Given(),
                When(new CreateBooking
                {
                    Id = testId,
                    RoomNumber = roomNumber,
                    RoomTarrifPerNight = roomTarrif,
                    CheckinDate = checkinDate,
                    CheckoutDate = checkoutDate,
                    //Client = client,
                    StaffId = staffId
                }),
                Then(new BookingCreated
                {
                    Id = testId,
                    RoomNumber = roomNumber,
                    RoomTarrifPerNight = roomTarrif,
                    CheckinDate = checkinDate,
                    CheckoutDate = checkoutDate,
                    Client = client,
                    StaffId = staffId
                }));
        }

        [TestMethod]
        public void CanNotCheckinWithoutABooking()
        {
            Test(
                Given(),
                When(new Checkin
                {
                    Id = testId,
                    ActualCheckinDate=checkinDate
                }),
                ThenFailWith<BookingNotCreated>());
        }

        [TestMethod]
        public void CanNotCheckoutWithoutCheckingIn()
        {
            Test(
                Given(),
                When(new Checkout
                {
                    Id = testId,
                    ActualCheckoutDate = checkoutDate
                }),
                ThenFailWith<CheckinNotHappened>());
        }
    }
}
