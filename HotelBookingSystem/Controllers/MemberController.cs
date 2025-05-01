using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Controllers
{
    public class MemberController : OrderBaseController
    {
        public IActionResult Member()
        {
            return View();
        }

        public IActionResult Bonding()
        {
            return View();
        }
        public IActionResult Order()
        {
            return View();
        }
    }
}
