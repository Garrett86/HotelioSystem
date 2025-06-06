using HotelBookingSystem.Data;
using HotelBookingSystem.Data.Entities;
using HotelBookingSystem.Models.DTO;

namespace HotelBookingSystem.Services.Hotel
{
    public interface IMemberRepository
    {
        Task<Member> GetUserByAccountAsync(string account);
        Task<Member> GetUserByMemberIdAsync(string id);
        //Task<List<Member>> SearchMembers(Member_Data_Search Member_Search);

        Task<Member> SaveMemberAsync(Member_Data_Edit Member_Eidit);
    }
}
