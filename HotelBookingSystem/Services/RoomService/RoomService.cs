using Dapper;
using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DB;
using HotelBookingSystem.Models.DTO;
using HotelBookingSystem.Repositories.RoomRepositories;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelBookingSystem.Services.RoomService
{
    public class RoomService : ServiceBase<Room>, IRoomService
    {
        private readonly IRoomRepository _room;
        public RoomService(HotelBookingDbContext context, IRoomRepository roomRepository) : base(context)
        {
            _room = roomRepository;
        }

        public enum Action_Type
        {
            [Display(Name = "新增")]
            Insert,
            [Display(Name = "修改")]
            Update,
            [Display(Name = "刪除")]
            Delete,
        }

        public async Task<IEnumerable<Room_Data_Table>> SearchRooms(Room_Data_Search Room_Search)
        {
            var result =  await _room.SearchRooms(Room_Search);

            return result;
        }

        public Room GetRoomById(int id)
            => GetDataOrDefaultByPKey(id);
    }
}
