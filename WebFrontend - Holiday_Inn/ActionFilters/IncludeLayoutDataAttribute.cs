using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebFrontend_Holiday_Inn.ActionFilters
{
    public class IncludeLayoutDataAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (filterContext.Result is ViewResult)
            {
                var bag = (filterContext.Result as ViewResult).ViewBag;
                bag.WaitStaff = StaticData.BookingStaff;
                bag.ActiveTables = Domain.OpenBookingQueries.BookedRoomNumbers();
            }
        }
    }
}