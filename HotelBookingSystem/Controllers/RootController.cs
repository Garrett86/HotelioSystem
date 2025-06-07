using HotelBookingSystem.Data;
using HotelBookingSystem.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using Microsoft.EntityFrameworkCore;
using HotelBookingSystem.Services.RoomService;
using HotelBookingSystem.Services.RootService;
using HotelBookingSystem.Models.ViewModel;

namespace HotelBookingSystem.Controllers
{
  [ApiController]
[Route("api/Root")]
    public class RootController : OrderBaseController
    {

        private readonly IRootService _root;

        public RootController(IRootService root)
        {
            _root = root;
        }

        [HttpGet("rootpage")]
        public IActionResult RootPage()
        {
            return View();
        }

        [HttpGet("SearchByRoom")]
        public async Task<ActionResult<RoomSearchViewModel>> GetAllRoom([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = await _root.GetAllRoom(page, pageSize);
            return Ok(result);
        }
    }
}
