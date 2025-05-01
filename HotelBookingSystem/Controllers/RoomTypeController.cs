using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Controllers
{
    public class RoomTypeController : Controller
    {
        public IActionResult RoomType()
        {
            return View();
        }
    }
}
