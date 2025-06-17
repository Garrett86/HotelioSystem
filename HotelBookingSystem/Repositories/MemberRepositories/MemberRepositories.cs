using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DB;

namespace HotelBookingSystem.Repositories.MemberRepositories
{
    public class MemberRepositories : RepositeriesBase<Member>, IMemberRepositories
    {
        public MemberRepositories(HotelBookingDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Member>> SearchAllMember()
        {
            var sql = "SELECT * FROM Booking";
            var result = await ExecuteQuery<Member>(sql);
            return result;
        }
    }
}
