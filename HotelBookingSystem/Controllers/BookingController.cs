using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Controllers
{
    public class BookingController : OrderBaseController
    {
        public IActionResult Booking()
        {
            return View();
        }
    }
}
