using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult News()
        {
            return View();
        }
    }
}
