using HolidayInn.Booking;
using CafeReadModels;
using Framework.CQRS;

namespace WebFrontend_Holiday_Inn
{
    public static class Domain
    {
        public static MessageDispatcher Dispatcher;
        public static IOpenBookingQueries OpenBookingQueries;
      

        public static void Setup()
        {
            Dispatcher = new MessageDispatcher(new InMemoryEventStore());
            
            Dispatcher.ScanInstance(new BookingAggregate());

            OpenBookingQueries = new OpenBookings();
            Dispatcher.ScanInstance(OpenBookingQueries);

            
        }
    }
}