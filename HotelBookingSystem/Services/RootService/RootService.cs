using AutoMapper;
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
        private readonly IMapper _mapper;

        public RootService(HotelBookingDbContext context, ILogger<RootService> logger, IRoomService roomService, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _room = roomService;
            _mapper = mapper;
        }

        public async Task<RoomSearchViewModel> GetAllRoom(int? vacantRoom, int page, int pageSize)
        {
            try
            {
                if (page <= 0) page = 1;
                if (pageSize <= 0) pageSize = 15;
                var query = await this._room.getAllRoomsByData();
                if (vacantRoom == 1)
                {
                    query = query.Where(x => x.vacantRoom == vacantRoom).ToList();
                }
                else if (vacantRoom == 0 && vacantRoom != null)
                {
                    query = query.Where(x => x.vacantRoom == vacantRoom).ToList();
                }

                var data = query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();


                var dataresult = this._mapper.Map<IEnumerable<Room>>(data);

                var result = new RoomSearchViewModel
                {
                    Page = page,
                    PageSize = pageSize,
                    Items = dataresult
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
