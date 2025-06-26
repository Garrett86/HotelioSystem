using AutoMapper;
using HotelBookingSystem.Data.Entities;
using HotelBookingSystem.Models.DTO;
using HotelBookingSystem.Models.ViewModel;
using HotelBookingSystem.Services.BookingService;
using HotelBookingSystem.Services.RoomService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
            return View("BookPage");
        }

        public IActionResult ShoppingCart()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ShoppingCart(Book_Data book)
        {
            var user = Request.Cookies["UserAccount"];
            if (string.IsNullOrEmpty(user))
            {
                TempData["ErrorMessage"] = "請先登入再進行訂房";
                return RedirectToAction("BookPage", "Book");
            }
            book.userName = user;
            book.BookingDate = DateTime.Now;
            var count = await this._room.GetAvailableRooms(book.bookingId, book.PeopleCount);

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DelectBookingByData()
        {
            var userName = Request.Cookies["UserAccount"] ?? "";
            await _booking.DeleteBookingByName(userName);
            TempData["SuccessMessage"] = "刪除成功!";
            return RedirectToAction("Order", "Member");
        }
    }
}
