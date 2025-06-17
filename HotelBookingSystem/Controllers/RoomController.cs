using HotelBookingSystem.Data.Entities;
using HotelBookingSystem.Models.DTO;
using HotelBookingSystem.Services.RoomService;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _room;
        public RoomController(IRoomService room)
        {
            _room = room;
        }
        public IActionResult SearchRoom()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Room_Data_QueryM(Room_Data_Search Room_Search)
        {
            var room_data_Table = await _room.SearchRooms(Room_Search);
            return PartialView("_Room_Table", room_data_Table);
        }

    }
}
