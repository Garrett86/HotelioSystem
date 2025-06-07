using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Controllers
{
    public class InfoController : Controller
    {
        public IActionResult Info()
        {
            return View();
        }
    }
}
