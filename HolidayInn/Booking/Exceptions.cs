using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HolidayInn.Booking
{
    public class BookingNotCreated : Exception
    {
    }

    public class CheckinNotHappened : Exception
    {
    }

    public class MustPayEnough : Exception
    {
    }

    public class CannotCancelAfterCheckingOut : Exception
    {
    }


}
