using Dapper;
using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DB;
using HotelBookingSystem.Models.DTO;
using HotelBookingSystem.Repositories.RoomRepositories;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text;
using HotelBookingSystem.Services.Enums;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using HotelBookingSystem.Models.ViewModel;

namespace HotelBookingSystem.Services.RoomService
{
    public class RoomService : ServiceBase<Room>, IRoomService
    {
        private readonly IRoomRepository _room;
        private readonly IMapper _mapper;
        private readonly ILogger<RoomService> _logger;
        public RoomService(HotelBookingDbContext context, IRoomRepository roomRepository, IMapper mapper, ILogger<RoomService> logger) : base(context)
        {
            _room = roomRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<Room_Data_Table>> SearchRooms(Room_Data_Search Room_Search)
        {
            var result = await _room.SearchRooms();
            result = result.Where(x => x.vacantRoom == 1).ToList();
            if (Room_Search.capacity != 0)
            {
                result = result.Where(x => x.capacity == Room_Search.capacity).ToList();
            }

            return result;
        }

        public Room GetRoomById(int id)
            => GetDataOrDefaultByPKey(id);

        public RoomEidtViewModel  Save(Room_Data_Edit Data_Edit, Action_Type eAction_Type)
        {
            string sMessage = "";
            List<string> liError = new List<string>();
            bool isSuccess = false;
            List<Room_Data_Edit> result = new();

            switch (eAction_Type)
            {
                case Action_Type.Insert:
                case Action_Type.Update:
                    if (Data_Edit.roomType.IsNullOrEmpty())
                    {
                        liError.Add("請輸入房間");
                    }
                    break;
            }

            if (!liError.Any())
            {
                try
                {
                    var room_Data = GetRoomById(Data_Edit.RoomId);
                    switch (eAction_Type)
                    {
                        case Action_Type.Insert:
                            room_Data = new Room();
                            room_Data = this._mapper.Map(Data_Edit, room_Data);
                            break;
                        case Action_Type.Update:
                            room_Data = this._mapper.Map(Data_Edit, room_Data);
                            break;
                        case Action_Type.Delete:
                            room_Data = this._mapper.Map(Data_Edit, room_Data);
                            break;

                    }
                    this.Update(room_Data, eAction_Type);
                    isSuccess = true;
                    sMessage = $"{eAction_Type.GetDisplayName()}成功";

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "發生例外錯誤：{Message}", ex.Message);
                    sMessage = "儲存失敗，請洽系統管理員";
                }
            }
            else
            {
                sMessage += string.Join("\n", liError);
            }
            return new RoomEidtViewModel
            {
                IsSuccess = isSuccess,
                message = sMessage
            };
        }


        protected void Update(Room room, Action_Type eAction_Type)
        {
            try
            {
                switch (eAction_Type)
                {
                    case Action_Type.Insert:
                        this.SaveInputColumn(room);
                        break;
                    case Action_Type.Update:
                        this.SaveInputColumn(room);
                        break;
                    case Action_Type.Delete:
                        this.DeleteColumn(room);
                        break;
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<Room> getAllRooms()
        {
            return db.Rooms.AsQueryable();
        }
    }
}