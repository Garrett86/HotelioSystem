using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DB;
using HotelBookingSystem.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace HotelBookingSystem.Services.RootService
{
    public class RootService : IRootService
    {
        private readonly HotelBookingDbContext _context;
        private readonly ILogger<RootService> _logger;

        public RootService(HotelBookingDbContext context, ILogger<RootService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<RoomSearchViewModel> GetAllRoom(int page, int pageSize)
        {
            try
            {
                if (page <= 0) page = 1;
                if (pageSize <= 0) pageSize = 10;

                var query = _context.Rooms.Where(r => r.vacantRoom != 1);

                var data = await query
                    .OrderByDescending(r => r.createdAt)
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
