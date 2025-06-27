using AutoMapper;
using HotelBookingSystem.Data.Entities;
using HotelBookingSystem.Models.DTO;
using HotelBookingSystem.Models.ViewModel;
using HotelBookingSystem.Services.BookingService;
using HotelBookingSystem.Services.RoomService;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
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

        public async Task<IActionResult> ShoppingCart()
        {
            var user = Request.Cookies["UserAccount"];
            if (string.IsNullOrEmpty(user))
            {
                TempData["ErrorMessage"] = "請先登入再進行訂房";
                return RedirectToAction("BookPage", "Book");
            }
            var bookings = await this._booking.BookingByaccount(user);
            var data = await this._booking.GetBookByNewDatas(user);
            ViewData["BookingData"] = bookings;
            ViewData["RoomData"] = data;
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
            book.totalAmount = book.price * book.PeopleCount;

            var test = await this._booking.BookingByaccount(user);
            if (test != null )
            {
                bool hasRoom = test.Any(b => b.roomId == book.roomId);
                if (hasRoom) {
                    TempData["ErrorMessage"] = "訂單重複請重新選擇";
                    return RedirectToAction("BookPage", "Book");
                }
            }

            var count = await this._room.GetAvailableRooms(book.bookingId, book.PeopleCount);

            var bookaccount = await this._booking.SaveAnync(book);
            var booking = await this._booking.GetBookByAccountOnShoping(user);
            var room = new Room_Data_Table();
            room = await this._booking.GetBookByNewData(user);
            var bookings = booking != null ? new List<Book_Data_Search> { booking } : Enumerable.Empty<Book_Data_Search>();
            var data = room != null ? new List<Room_Data_Table> { room } : Enumerable.Empty<Room_Data_Table>();
            ViewData["BookingData"] = bookings;
            ViewData["RoomData"] = data;
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
        public async Task<IActionResult> DelectBookingByData(int id)
        {
            await this._booking.DeleteBookingByRoomId(id);
            TempData["SuccessMessage"] = "刪除成功!";
            return RedirectToAction("Order", "Member");
        }
    }
}
