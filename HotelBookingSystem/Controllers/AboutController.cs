using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult About()
        {
            return View();
        }
    }
}
