using HotelBookingSystem.Models;

namespace HotelBookingSystem.Repositories.CodeItemRepository
{
    public interface ICodeItemRepository
    {
        Task<IEnumerable<CodeItem>> GetAllCodeItem();
    }
}
