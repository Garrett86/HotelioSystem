using HotelBookingSystem.Models.DTO;

namespace HotelBookingSystem.Repositories.RoomRepositories
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room_Data_Table>> SearchRooms();

        Task<IEnumerable<Room_Data_Table>> SearchRoomByIdAnync(int id);

        Task UpdateRoomByVacantRoomAnync(int id,int vacantRoom);
    }
}
