using AutoMapper;
using HotelBookingSystem.ActionFilter;
using HotelBookingSystem.Controllers;
using HotelBookingSystem.Data;
using HotelBookingSystem.Data.Entities;
using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DTO;
using HotelBookingSystem.Models.ViewModel;
using HotelBookingSystem.Services.RoomService;
using HotelBookingSystem.Services.RootService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.Contracts;
using Action_Type = HotelBookingSystem.Services.Enums.Action_Type;

namespace HotelBookingSystem.ApiControllers
{
    [ApiController]
    [Route("api/rooms")]
    public class RootController : RootBaseController
    {

        private readonly IRootService _root;
        private readonly IRoomService _room;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<RootController> _logger;

        public RootController(IConfiguration configuration, IRootService root, IRoomService room, IMapper mapper, ILogger<RootController> logger)
        {
            _configuration = configuration;
            _root = root;
            _room = room;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<RoomSearchViewModel>> GetAllRoom([FromQuery] int? vacantRoom, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _root.GetAllRoom(vacantRoom, page, pageSize);
            return Ok(result);
        }


        /// <summary>
        /// 重置房間資料（刪除 + 重建）
        /// POST /api/rooms/reset
        /// </summary>
        [HttpPost("reset")]
        public IActionResult ResetRooms()
        {
            try
            {
                var sqlPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "sql", "reset_rooms.sql");
                string sql = System.IO.File.ReadAllText(sqlPath);
                using var conn = new SqlConnection(_configuration.GetConnectionString("HotelBookingConnection"));
                conn.Open();
                using var cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                return Ok(new { success = true, message = "房間資料已成功重置。" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "房間重置失敗");
                return StatusCode(500, new { success = false, message = "房間資料重置失敗：" + ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteById(int id) {
            try
            {
                var data = this._room.GetRoomById(id);
                var datamapper = this._mapper.Map<Room_Data_Edit>(data);
                var result = this._room.Save(datamapper, Action_Type.Delete);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "刪除房間失敗");
                return StatusCode(500, "伺服器內部錯誤");
            }
        }
    }
}
