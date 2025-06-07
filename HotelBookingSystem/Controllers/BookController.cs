using AutoMapper;
using HotelBookingSystem.Data.Entities;
using HotelBookingSystem.Models.DTO;
using HotelBookingSystem.Models.ViewModel;
using HotelBookingSystem.Services.RoomService;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Controllers
{
    public class BookController : OrderBaseController
    {
        private readonly IRoomService _room;

        public BookController(IRoomService roomRepository)
        {
            _room = roomRepository;
        }
        public IActionResult BookPage()
        {
            if (Request.Cookies.ContainsKey("UserAccount"))
            {
                ViewBag.User = Request.Cookies["UserAccount"];
            }
            else
            {
                ViewBag.User = null;
            }
            return View();
        }

        public IActionResult ShoppingCart()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ShoppingCart(string date, int name)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Room_Data_QueryM(Room_Data_Search Room_Search)
        {
            var rooms = await _room.SearchRooms(Room_Search);
            ViewData["RoomList"] = rooms;
            return PartialView("BookPage");
        }
    }
}
