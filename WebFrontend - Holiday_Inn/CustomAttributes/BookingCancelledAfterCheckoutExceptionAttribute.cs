using HolidayInn.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace WebFrontend.CustomAttributes
{
    public class BookingCancelledAfterCheckoutExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if(!filterContext.ExceptionHandled && filterContext.Exception is CannotCancelAfterCheckingOut)
            {
                filterContext.Result = new RedirectResult("~/Content/BookingCancellationError.html");
                filterContext.ExceptionHandled = true;
            }
        }
    }
}