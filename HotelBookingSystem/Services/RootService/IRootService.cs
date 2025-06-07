using HotelBookingSystem.Models.DTO;
using HotelBookingSystem.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Services.RootService
{
    public interface IRootService
    {
        Task<RoomSearchViewModel> GetAllRoom([FromQuery] int page, [FromQuery] int pageSize);
    }
}
