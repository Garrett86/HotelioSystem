using Dapper;
using HotelBookingSystem.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Repositories
{
    public class RepositeriesBase<T> where T : class, new()
    {
        protected readonly HotelBookingDbContext db;

        public RepositeriesBase(HotelBookingDbContext context)
        {
            this.db = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected async Task <List<T>> ExecuteQuery<T>(string sql, object parameters = null)
        {
            try
            {
                using (var conn = db.Database.GetDbConnection())
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryAsync<T>(sql, parameters);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                // 可記錄錯誤，或拋出自訂例外
                throw new ApplicationException("資料庫查詢失敗", ex);
            }
        }
    }
}
