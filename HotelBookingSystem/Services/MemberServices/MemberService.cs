using Dapper;
using HotelBookingSystem.Data.Entities;
using HotelBookingSystem.Models.DB;
using HotelBookingSystem.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text;

namespace HotelBookingSystem.Services.Hotel
{
    public class MemberService : ServiceBase<Member>, IMemberRepository
    {
        public MemberService(HotelBookingDbContext context) : base(context)
        {
        }

        protected void ExecuteNonQuery(string sqlQuery, object parameters = null)
        {
            using (var conn = db.Database.GetDbConnection())
            {
                conn.Open();  // 同步開啟連線
                conn.Execute(sqlQuery, parameters);
            }
        }



        public async Task<Member> GetUserByAccountAsync(string account)
            => await db.Members.FirstOrDefaultAsync(x => x.account == account);


        public async Task<Member> GetUserByMemberIdAsync(string id)
            => await db.Members.FirstOrDefaultAsync(x => x.memberid == id);



        public async Task<List<Member>> SearchMembers(Member_Data_Search Member_Search)
        {
            StringBuilder SQL = new StringBuilder("");
            SQL.AppendLine(" SELECT M.id");
            SQL.AppendLine("        ,M.memberid");
            SQL.AppendLine("        ,M.account");
            SQL.AppendLine("        ,M.password");
            SQL.AppendLine("        ,M.firstName");
            SQL.AppendLine("        ,M.lastName");
            SQL.AppendLine("        ,M.cardID");
            SQL.AppendLine("        ,M.email");
            SQL.AppendLine("        ,M.phone");
            SQL.AppendLine("        ,M.birthday");
            SQL.AppendLine("        ,M.gender");
            SQL.AppendLine("        ,M.nationality");
            SQL.AppendLine("        ,M.city");
            SQL.AppendLine("        ,M.initDate");
            SQL.AppendLine("FROM Members M");
            SQL.AppendLine("LEFT JOIN CodeItem CI ON M.gender = CI.ItemValue WHERE CI.CodeID = 'gender'");
            string sql = SQL.ToString();
            var result = GetAllMemberAsync(sql);
            if (!string.IsNullOrEmpty(Member_Search.account))
                result = result.Where(x => x.account == Member_Search.account).ToList();
            return result;
        }

        protected  List<Member> GetAllMemberAsync(string sql)
        {
            using (var conn = db.Database.GetDbConnection())
            {
                 conn.Open();
                var members =conn.Query<Member>(sql);
                return members.AsList();
            }
        }

        public async Task<Member> SaveMemberAsync(Member_Data_Edit Member_Eidit)
        {
            var member = await GetUserByAccountAsync(Member_Eidit.account);
            if (member == null)
                throw new Exception("找不到該會員資料");

            // 更新會員資料
            member.firstName = Member_Eidit.firstName;
            member.lastName = Member_Eidit.lastName;
            member.cardID = Member_Eidit.cardID;
            member.gender = Member_Eidit.gender;
            member.nationality = Member_Eidit.nationality;
            member.city = Member_Eidit.city;

            // 處理密碼更新
            if (!string.IsNullOrEmpty(Member_Eidit.password))
            {
                if (Member_Eidit.password == Member_Eidit.passwordConfirm)
                {
                    member.password = Member_Eidit.password; // 更新密碼
                }
                else
                {
                    throw new Exception("密碼與確認密碼不一致");
                }
            }

            // 儲存變更
            await db.SaveChangesAsync();

            return member;
        }
    }
}
