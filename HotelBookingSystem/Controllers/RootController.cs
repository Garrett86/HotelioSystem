using AutoMapper;
using HotelBookingSystem.ActionFilter;
using HotelBookingSystem.Models.DTO;
using HotelBookingSystem.Services.Enums;
using HotelBookingSystem.Services.RoomService;
using Microsoft.AspNetCore.Mvc;
using static HotelBookingSystem.Services.RoomService.RoomService;

namespace HotelBookingSystem.Controllers
{
    public class RootController : RootBaseController
    {

        private readonly IRoomService _room;
        private readonly IMapper _mapper;

        public RootController(IRoomService room, IMapper mapper)
        {
            _room = room;
            _mapper = mapper;
        }

        public IActionResult RootPage()
        {
            return View();
        }


        [HttpGet]
        //[CheckPoint(PermissionType.Update)]
        public async Task<ActionResult<Room_Data_Edit>> GetByIdRoom(int id)
        {
            try
            {
                var roomDataEdit = await Room_Data_QueryDAsync(id, Action_Type.Update);
                if (roomDataEdit == null)
                {
                    return NotFound();
                }
                return PartialView("_RootTable", roomDataEdit); // 傳給 View
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
            
        }

        /// <summary>
        /// 依照編輯類型帶出明細資料
        /// </summary>
        /// <param name="sUserID"></param>
        /// <param name="sAction_Type"></param>
        /// <returns></returns>
        private async Task<Room_Data_Edit> Room_Data_QueryDAsync(int sRoomID, Action_Type eAction_Type)
        {
            var cRoom_Data_Edit = new Room_Data_Edit();
            if (eAction_Type != Action_Type.Insert)
            {
                var rooms = _room.GetRoomById(sRoomID);
                if (rooms == null)
                {
                    return null;
                }
                cRoom_Data_Edit = _mapper.Map(rooms, cRoom_Data_Edit);
            }
            return cRoom_Data_Edit;
        }
    }
}
