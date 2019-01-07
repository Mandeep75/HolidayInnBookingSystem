using System;
using System.Collections.Generic;

namespace CafeReadModels
{
    public interface IOpenBookingQueries
    {
        List<int> BookedRoomNumbers();
        OpenBookings.BookingInvoice InvoiceForRoom(int roomNumber);
        Guid BookingIdForRoomNumber(int roomNumber);
        OpenBookings.BookingStatus BookingForRoomNumber(int roomNumber);
        List<OpenBookings.BookingStatus> GetAllBookingsList();

    }
}
