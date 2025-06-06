using HotelBookingSystem.Data.Entities;

namespace HotelBookingSystem.Services.CodeItemService
{
    public interface ICodeItemRepository
    {
        Task<List<CodeItem>> GetgenderByID();
    }
}
