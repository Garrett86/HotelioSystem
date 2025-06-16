using AutoMapper;
using HotelBookingSystem.Data.Entities;
using HotelBookingSystem.Models.DTO;
using HotelBookingSystem.Models.ViewModel;
using HotelBookingSystem.Services.BookingService;
using HotelBookingSystem.Services.RoomService;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Controllers
{
    public class BookController : OrderBaseController
    {
        private readonly IRoomService _room;
        private readonly IBookingService _booking;

        public BookController(IRoomService room, IBookingService booking)
        {
            _room = room;
            _booking = booking;
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
        public IActionResult ShoppingCart(Book_Data book)
        {
            var user = Request.Cookies["UserAccount"];
            if (string.IsNullOrEmpty(user))
            {
                TempData["ErrorMessage"] = "請先登入再進行訂房";
                return RedirectToAction("BookPage", "Book");
            }
            book.UserName = user;
            book.BookingDate = DateTime.Now;

            var booking = _booking.SaveAnync(book);
            ViewData["BookData"] = book;
           return View();
        }

        [HttpPost]
        public async Task<IActionResult> Room_Data_QueryM(Room_Data_Search Room_Search)
        {
            if (Request.Cookies.ContainsKey("UserAccount"))
            {
                ViewBag.User = Request.Cookies["UserAccount"];
            }
            else
            {
                ViewBag.User = null;
            }
            var rooms = await _room.SearchRooms(Room_Search);
            ViewData["RoomList"] = rooms;
            return PartialView("BookPage");
        }
    }
}
