using HotelBookingSystem.Models.DTO;

namespace HotelBookingSystem.Repositories.RoomRepositories
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room_Data_Table>> SearchRooms();
    }
}
