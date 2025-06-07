using HotelBookingSystem.Models;

namespace HotelBookingSystem.Services.CodeItemService
{
    public interface ICodeItemRepository
    {
        Task<List<CodeItem>> GetgenderByID();
    }
}
