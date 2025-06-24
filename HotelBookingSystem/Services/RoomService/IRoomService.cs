using HotelBookingSystem.Data.Entities;
using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DTO;
using HotelBookingSystem.Models.ViewModel;
using HotelBookingSystem.Services.Enums;

namespace HotelBookingSystem.Services.RoomService
{
    public interface IRoomService
    {
        Task<IEnumerable<Room_Data_Table>> SearchRooms(Room_Data_Search Room_Search);
        Room GetRoomById(int id);

        IQueryable<Room> getAllRooms();

        RoomEidtViewModel Save(Room_Data_Edit Data_Edit, Action_Type eAction_Type);
    }
}
