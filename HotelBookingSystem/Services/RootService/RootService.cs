using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DB;
using HotelBookingSystem.Models.ViewModel;
using HotelBookingSystem.Services.RoomService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace HotelBookingSystem.Services.RootService
{
    public class RootService : IRootService
    {
        private readonly HotelBookingDbContext _context;
        private readonly ILogger<RootService> _logger;
        private readonly IRoomService _room;

        public RootService(HotelBookingDbContext context, ILogger<RootService> logger,IRoomService roomService)
        {
            _context = context;
            _logger = logger;
            _room= roomService;
        }

        public async Task<RoomSearchViewModel> GetAllRoom(int? vacantRoom, int page,  int pageSize)
        {
            try
            {
                if (page <= 0) page = 1;
                if (pageSize <= 0) pageSize = 10;
                var query = this._room.getAllRooms();
                if (vacantRoom == 1)
                {
                    query = query.Where(x => x.vacantRoom == vacantRoom);
                }

                var data = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var result = new RoomSearchViewModel
                {
                    Page = page,
                    PageSize = pageSize,
                    Items = data
                };
                return result;
            }
            catch (Exception ex)
            {
                // 記錄錯誤，方便除錯
                // 例如使用日誌工具：_logger.LogError(ex, "Error loading rooms");
                _logger.LogError(ex, "Error loading rooms");

                return new RoomSearchViewModel
                {
                    Page = page,
                    PageSize = pageSize,
                    Items = new List<Room>() // 這裡返回一個空的房間列表
                };
            }
        }
    }
}
