using HotelBookingSystem.Data;
using HotelBookingSystem.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Controllers
{
  [ApiController]
[Route("api/Root")]
    public class RootController : OrderBaseController
    {
        private readonly HotelBookingDbContext _dbContext;

        public RootController(HotelBookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("rootpage")]
        public IActionResult RootPage()
        {
            return View();
        }

        [HttpGet("SearchByRoom")]
        public async Task<IActionResult> GetAllRoom([FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                if (page <= 0) page = 1;
                if (pageSize <= 0) pageSize = 10;

                var query = _dbContext.Rooms.Where(r => r.vacantRoom != 1);

                var data = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var result = new
                {
                    Page = page,
                    PageSize = pageSize,
                    Items = data
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                // 記錄錯誤，方便除錯
                // 例如使用日誌工具：_logger.LogError(ex, "Error loading rooms");
                return StatusCode(500, new { message = "Internal server error", detail = ex.Message });
            }
        }


    }
}
