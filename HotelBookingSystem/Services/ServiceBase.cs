using Dapper;
using HotelBookingSystem.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Services
{
    public class ServiceBase<T> where T :class,new()
    {
        protected readonly HotelBookingDbContext db;

        public ServiceBase(HotelBookingDbContext context)
        {
            db = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected List<T> ExecuteQuery<T>(string sql, object parameters = null)
        {
            try
            {
                using (var conn = db.Database.GetDbConnection())
                {
                    conn.Open();
                    var result = conn.Query<T>(sql, parameters);
                    return result.AsList();
                }
            }
            catch (Exception ex)
            {
                // 可記錄錯誤，或拋出自訂例外
                throw new ApplicationException("資料庫查詢失敗", ex);
            }
        }

        /// <summary>
        /// 如果找不到的話會回傳一個空物件
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public T GetDataOrDefaultByPKey(params object[] ID) => db.Set<T>().Find(ID) ?? Activator.CreateInstance<T>();//簡寫fun 
    }
}
