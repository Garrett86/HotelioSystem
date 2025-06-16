using Dapper;
using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DB;

namespace HotelBookingSystem.Repositories.CodeItemRepository
{
    public class CodeItemRepository : RepositeriesBase<CodeItem>, ICodeItemRepository
    {
        public CodeItemRepository(HotelBookingDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CodeItem>> GetAllCodeItem()
        {
            var sql = "SELECT * FROM CodeItem";
            var result = await ExecuteQuery<CodeItem>(sql);
            return result.ToList();
        }

    }
}
