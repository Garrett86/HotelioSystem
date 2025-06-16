using HotelBookingSystem.Data.Entities;
using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DTO;

namespace HotelBookingSystem.Services.RoomService
{
    public interface IRoomService
    {
        Task<IEnumerable<Room_Data_Table>> SearchRooms(Room_Data_Search Room_Search);
        Room GetRoomById(int id);
    }
}
