using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Services.CodeItemService
{
    public class GenderService : ServiceBase<CodeItem>, ICodeItemRepository
    {
        public GenderService(HotelBookingDbContext context) : base(context)
        {

        }
        public async Task<List<CodeItem>> GetgenderByID()
             => await db.CodeItems.Where(x => x.CodeID == "gender").OrderBy(x => x.ItemValue).ToListAsync();
    }
}
