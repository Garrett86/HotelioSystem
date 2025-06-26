using Dapper;
using HotelBookingSystem.Data.Entities;
using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DB;
using HotelBookingSystem.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.Text;
using HotelBookingSystem.Services.Enums;
using System.Threading.Tasks;
using AutoMapper;

namespace HotelBookingSystem.Services.MemberService
{
    public class MemberService : ServiceBase<Member>, IMemberService
    {
        private readonly IMapper _mapper;
        public MemberService(HotelBookingDbContext context, IMapper mapper, IConfiguration config) : base(context, config)
        {
            _mapper = mapper;
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

        public bool IsMemberIDExist(string MemberID)
            => db.Members.Where(x => x.memberid == MemberID).Any();

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

        protected List<Member> GetAllMemberAsync(string sql)
        {
            using (var conn = db.Database.GetDbConnection())
            {
                conn.Open();
                var members = conn.Query<Member>(sql);
                return members.AsList();
            }
        }

        public async Task<Member> SaveMemberAsync(Member_Data_Edit Member_Eidit)
        {
            var member = await GetUserByAccountAsync(Member_Eidit.account);
            if (member == null)
                throw new Exception("找不到該會員資料");

            // 更新會員資料，使用 AutoMapper 
            _mapper.Map(Member_Eidit, member);

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

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var msg = ex.InnerException?.Message ?? ex.Message;
                throw new Exception($"儲存變更時失敗：{msg}");
            }

            return member;
        }

        public Member GetDataOrDefaultByPKey(string id)
            => GetDataOrDefaultByPKey(id);

        public void Update(Member member, Action_Type eAction_Type)
        {
            try
            {
                switch (eAction_Type)
                {
                    case Action_Type.Insert:
                        SaveInputColumn(member);
                        AddOrUpdateAsync(member);
                        break;
                    case Action_Type.Update:
                        SaveUpdateColumn(member);
                        AddOrUpdateAsync(member);
                        break;
                    case Action_Type.Delete:
                        DeleteColumn(member);
                        break;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteAccount(Member_Data_Edit member_Data)
        {
            try
            {
                var memberId = member_Data.memberid;
                var member = await db.Members.FirstOrDefaultAsync(x => x.memberid == memberId);
                if(member is not null)
                {
                    return false;
                }
                db.Members.Remove(member);
                await db.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public string GenerateNextMember()
        {
            var maxMemberId = this.db.Members
                .OrderByDescending(m => m.memberid)
                .Select(m => m.memberid)
                .FirstOrDefault();

            int nexNumber = 1;
            if (!string.IsNullOrEmpty(maxMemberId) && maxMemberId.Length == 4 && maxMemberId.StartsWith("M"))
            {
                if (int.TryParse(maxMemberId.Substring(1), out int currentNumber))
                {
                    nexNumber = currentNumber + 1;
                    nexNumber = currentNumber + 1;
                }
            }

            return "M" + nexNumber.ToString("D3");
        }

        private async Task AddOrUpdateAsync(Member inputMember)
        {
            var existing = db.Members.FirstOrDefaultAsync(m => m.memberid == inputMember.memberid);
            if (existing != null)
            {
                // 更新現有資料
                db.Entry(existing).CurrentValues.SetValues(inputMember);
            }
            else
            {
                // 新增
                await db.Members.AddAsync(inputMember);
            }

            await db.SaveChangesAsync();
        }
    }
}
