using HolidayInn.Booking;
using System;
using System.Web.Mvc;
using WebFrontend.CustomAttributes;
using WebFrontend_Holiday_Inn.ActionFilters;

namespace WebFrontend_Holiday_Inn.Controllers
{
    [IncludeLayoutData]
    public class BookingController : Controller
    {
        public ActionResult Open()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Open(CreateBooking cmd)
        {
            cmd.Id = Guid.NewGuid();
            if (ModelState.IsValid)
            {
                Domain.Dispatcher.SendCommand(cmd);
                return RedirectToAction("Index", "BookingList");

            }
            
            return View();
            //return redirecttoaction("order", new { id = cmd. .tablenumber });
        }       
      
        [BookingCancelledAfterCheckoutException]
        public ActionResult Close(int id) {
          

            Domain.Dispatcher.SendCommand(new CancelBooking
            {
                Id = Domain.OpenBookingQueries.BookingIdForRoomNumber(id),
                //AmountPaid = model.AmountPaid
            });
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Checkout(int id)
        {
            
            Domain.Dispatcher.SendCommand(new Checkout
            {
                Id = Domain.OpenBookingQueries.BookingIdForRoomNumber(id),
                //AmountPaid = model.AmountPaid
            });
            return RedirectToAction("Index", "Home");
        }
        //[HttpPost]
        //public ActionResult Close(CancelModel model) {
        //    Domain.Dispatcher.SendCommand(new CancelBooking {
        //         Id = Domain.OpenBookingQueries.BookingIdForRoomNumber(model.Id)
        //    });
        //    return RedirectToAction("Index", "Home");
        //}

        public ActionResult Status(int id)
        {
            return View(Domain.OpenBookingQueries.BookingForRoomNumber (id));
        }

        [HttpPost]
        public ActionResult Status(object  model)
        {
            //Domain.Dispatcher.SendCommand(new CancelBooking
            //{
            //    Id = Domain.OpenBookingQueries.BookingIdForRoomNumber(model.Id)
            //});
            return RedirectToAction("Index", "Home");
        }

        //public ActionResult Order(int id)
        //{
        //    return View(new OrderModel
        //    {
        //        Items = (from item in StaticData.Menu
        //                 select new OrderModel.OrderItem
        //                 {
        //                     MenuNumber = item.MenuNumber,
        //                     Description = item.Description,
        //                     NumberToOrder = 0
        //                 }).ToList(),
        //    });
        //}

        //[HttpPost]
        //public ActionResult Order(int id, OrderModel order)
        //{
        //    var items = new List<Events.HolidayInn.OrderedItem>();
        //    var menuLookup = StaticData.Menu.ToDictionary(k => k.MenuNumber, v => v);
        //    foreach (var item in order.Items)
        //        for (int i = 0; i < item.NumberToOrder; i++)
        //            items.Add(new Events.HolidayInn.OrderedItem
        //            {
        //                MenuNumber = item.MenuNumber,
        //                Description = menuLookup[item.MenuNumber].Description,
        //                Price = menuLookup[item.MenuNumber].Price,
        //                IsDrink = menuLookup[item.MenuNumber].IsDrink
        //            });

        //    Domain.Dispatcher.SendCommand(new PlaceOrder
        //    {
        //        Id = Domain.OpenTabQueries.TabIdForTable(id),
        //        Items = items
        //    });

        //    return RedirectToAction("Status", new { id = id });
        //}


        //public ActionResult MarkServed(int id, FormCollection form)
        //{
        //    var tabId = Domain.OpenTabQueries.TabIdForTable(id);
        //    var menuNumbers = (from entry in form.Keys.Cast<string>()
        //                       where form[entry] != "false"
        //                       let m = Regex.Match(entry, @"served_\d+_(\d+)")
        //                       where m.Success
        //                       select int.Parse(m.Groups[1].Value)
        //                      ).ToList();

        //    var menuLookup = StaticData.Menu.ToDictionary(k => k.MenuNumber, v => v);

        //    var drinks = menuNumbers.Where(n => menuLookup[n].IsDrink).ToList();
        //    if (drinks.Any())
        //        Domain.Dispatcher.SendCommand(new MarkDrinksServed
        //        {
        //            Id = tabId,
        //            MenuNumbers = drinks
        //        });

        //    var food = menuNumbers.Where(n => !menuLookup[n].IsDrink).ToList();
        //    if (food.Any())
        //        Domain.Dispatcher.SendCommand(new MarkFoodServed
        //        {
        //            Id = tabId,
        //            MenuNumbers = food
        //        });

        //    return RedirectToAction("Status", new { id = id });
        //}

    }
}
