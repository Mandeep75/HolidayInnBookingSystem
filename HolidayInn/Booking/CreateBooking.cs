using Events.Booking;
using System;
using System.ComponentModel.DataAnnotations;

namespace HolidayInn.Booking
{
    public class CreateBooking
    {
        public Guid Id;
        [Range(1, 150)]
        public int RoomNumber { get; set; }
        public decimal RoomTarrifPerNight { get; set; }
        public Customer Client { get; set; }

        [Required(ErrorMessage = "Checkin Date is Required")]
        public DateTime CheckinDate { get; set; }
        [Required(ErrorMessage = "Checkout Date is Required")]
        public DateTime CheckoutDate { get; set; }
        public string StaffId { get; set; }
    }
}
