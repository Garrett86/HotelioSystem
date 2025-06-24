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
using Microsoft.EntityFrameworkCore;
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

        public RootController(IRootService root, IRoomService room, IMapper mapper)
        {
            _root = root;
            _room = room;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<RoomSearchViewModel>> GetAllRoom([FromQuery] int? vacantRoom, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _root.GetAllRoom(vacantRoom, page, pageSize);
            return Ok(result);
        }

    }
}
