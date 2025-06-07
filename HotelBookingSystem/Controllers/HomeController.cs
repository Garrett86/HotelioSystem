using HotelBookingSystem.Data;
using HotelBookingSystem.Data.Repositories;
using HotelBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HotelBookingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHotelBookingRepository<Customer, string> _hotelBookingRepository;

        public HomeController(ILogger<HomeController> logger, IHotelBookingRepository<Customer, string> hotelBookingRepository)
        {
            _logger = logger;
            _hotelBookingRepository = hotelBookingRepository;

        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
