using Dapper;
using HotelBookingSystem.Models.DB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HotelBookingSystem.Repositories
{
    public class RepositeriesBase<T> where T : class, new()
    {
        protected readonly HotelBookingDbContext db;
        private readonly string _connString;
        private HotelBookingDbContext context;

        public RepositeriesBase(HotelBookingDbContext context, IConfiguration configuration)
        {
            this.context = context;
            _connString = configuration.GetConnectionString("HotelBookingConnection");
        }


        protected async Task<List<T>> ExecuteQuery<T>(string sql, object parameters = null)
        {
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    //Console.WriteLine("連線字串：" + conn.ConnectionString);
                    await conn.OpenAsync();
                    var result = await conn.QueryAsync<T>(sql, parameters);
                    conn.Close();
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                // 可記錄錯誤，或拋出自訂例外
                Console.WriteLine("查詢失敗：" + ex.ToString());
                throw new ApplicationException("資料庫查詢失敗", ex);
            }
        }
    }
}
