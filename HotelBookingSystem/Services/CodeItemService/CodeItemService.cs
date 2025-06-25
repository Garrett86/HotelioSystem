using Dapper;
using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DB;
using HotelBookingSystem.Repositories.CodeItemRepository;

namespace HotelBookingSystem.Services.CodeItemService
{
    public class CodeItemService : ServiceBase<CodeItem>,ICodeItemService
    {
        private readonly ICodeItemRepository _codeItem;

        public CodeItemService(HotelBookingDbContext context, ICodeItemRepository codeItem) : base(context)
        {
            _codeItem = codeItem;
        }

        public async Task<IEnumerable<CodeItem>> GetCodeItemById()
        {
            var items = await _codeItem.GetAllCodeItem();
            var result = items.Where(x => x.CodeID == "gender").ToList();
            return result;
        }

        public async Task<IEnumerable<CodeItem>> GetCodeItemBynum()
        {
            var items = await _codeItem.GetAllCodeItem();
            var result = items.Where(x => x.CodeID == "vacantRoom").ToList();
            return result;
        }
    }
}
