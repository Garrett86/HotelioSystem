using Dapper;
using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DB;
using HotelBookingSystem.Models.DTO;
using HotelBookingSystem.Repositories.RoomRepositories;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text;
using HotelBookingSystem.Services.Enums;

namespace HotelBookingSystem.Services.RoomService
{
    public class RoomService : ServiceBase<Room>, IRoomService
    {
        private readonly IRoomRepository _room;
        public RoomService(HotelBookingDbContext context, IRoomRepository roomRepository) : base(context)
        {
            _room = roomRepository;
        }

        public async Task<IEnumerable<Room_Data_Table>> SearchRooms(Room_Data_Search Room_Search)
        {
            var result =  await _room.SearchRooms(Room_Search);
            if (Room_Search.capacity != 0)
            {
                result = result.Where(x => x.capacity == Room_Search.capacity);
            }

            return result;
        }

        public Room GetRoomById(int id)
            => GetDataOrDefaultByPKey(id);
    }
}
