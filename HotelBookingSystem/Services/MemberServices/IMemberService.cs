using HotelBookingSystem.Data;
using HotelBookingSystem.Data.Entities;
using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DTO;
using HotelBookingSystem.Services.Enums;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace HotelBookingSystem.Services.MemberService
{
    public interface IMemberService
    {
        Task<Member> GetUserByAccountAsync(string account);
        Task<Member> GetUserByMemberIdAsync(string id);
        //Task<List<Member>> SearchMembers(Member_Data_Search Member_Search);

        bool IsMemberIDExist(string MemberID);

        Task<Member> SaveMemberAsync(Member_Data_Edit Member_Eidit);

        Member GetDataOrDefaultByPKey(string id);

        void Update(Member member, Action_Type eAction_Type);

        Task<bool> DeleteAccount(Member_Data_Edit member_Data);


    }
}
