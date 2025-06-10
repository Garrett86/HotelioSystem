using Dapper;
using HotelBookingSystem.Models;
using HotelBookingSystem.Repositories.CodeItemRepository;

namespace HotelBookingSystem.Services.CodeItemService
{
    public class CodeItemService : ICodeItemService
    {
        private readonly ICodeItemRepository _codeItem;
        public CodeItemService(ICodeItemRepository codeItem)
        {
            _codeItem = codeItem;
        }

        public async Task<IEnumerable<CodeItem>> GetCodeItemById()
        {
            var items = await _codeItem.GetAllCodeItem();
            var result = items.Where(x => x.CodeID == "gender").ToList();
            return result;
        }
    }
}
