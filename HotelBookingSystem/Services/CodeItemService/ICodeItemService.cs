using HotelBookingSystem.Models;

namespace HotelBookingSystem.Services.CodeItemService
{
    public interface ICodeItemService
    {
        Task<IEnumerable<CodeItem>> GetCodeItemById();
        Task<IEnumerable<CodeItem>> GetCodeItemBynum();
    }
}
