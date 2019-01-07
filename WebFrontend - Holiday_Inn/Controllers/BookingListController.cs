using System.Web.Mvc;
using WebFrontend_Holiday_Inn.ActionFilters;

namespace WebFrontend_Holiday_Inn.Controllers
{
    [IncludeLayoutData]
    public class BookingListController : Controller
    {
        public ActionResult Index()
        {
            return View(Domain.OpenBookingQueries.GetAllBookingsList());
        }

        //public ActionResult MarkPrepared(Guid id, FormCollection form)
        //{
        //    Domain.Dispatcher.SendCommand(new MarkFoodPrepared
        //    {
        //        Id = id,
        //        MenuNumbers = (from entry in form.Keys.Cast<string>()
        //                       where form[entry] != "false"
        //                       let m = Regex.Match(entry, @"prepared_\d+_(\d+)")
        //                       where m.Success
        //                       select int.Parse(m.Groups[1].Value)
        //                      ).ToList()
        //    });

        //    return RedirectToAction("Index");
        //}
    }
}
