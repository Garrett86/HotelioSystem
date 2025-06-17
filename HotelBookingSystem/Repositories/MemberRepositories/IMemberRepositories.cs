using HotelBookingSystem.Models;

namespace HotelBookingSystem.Repositories.MemberRepositories
{
    public interface IMemberRepositories
    {
        Task<IEnumerable<Member>> SearchAllMember();
    }
}
