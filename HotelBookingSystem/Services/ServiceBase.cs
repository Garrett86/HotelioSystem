using Dapper;
using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Services
{
    public class ServiceBase<T> where T :class,new()
    {
        protected readonly HotelBookingDbContext db;
        protected IBaseInputColumn _baseInputColumn;

        public ServiceBase(HotelBookingDbContext context, IBaseInputColumn baseInputColumn)
        {
            db = context ?? throw new ArgumentNullException(nameof(context));
            _baseInputColumn = baseInputColumn;
        }

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
        /// 針對修改欄位
        /// </summary>
        public void SaveUpdateColumn(T entity)
        {
            var type = entity.GetType();
            if (typeof(IUpdatable).IsAssignableFrom(type)){
                var updatable = (IUpdatable)entity;
                updatable.InputUser = _baseInputColumn.CurrentUser;
                updatable.ModifyDate = _baseInputColumn.Now;
            }
        }

        /// 針對刪除欄位
        /// </summary>
        public void DeleteColumn(T entity)
        {
            var type = entity.GetType();
            if (typeof(IUpdatable).IsAssignableFrom(type))
            {
                var updatable = (IUpdatable)entity;
                updatable.ModifyUser = _baseInputColumn.CurrentUser;
                updatable.ModifyDate = _baseInputColumn.Now;
            }
            db.Set<T>().Remove(entity);
        }


        /// <summary>
        /// 針對新增欄位
        /// </summary>
        public void SaveInputColumn(T entity)
        {
            var type = entity.GetType();
            if (typeof(IUpdatable).IsAssignableFrom(type))
            {
                var updatable = (IUpdatable)entity;
                updatable.InputUser = _baseInputColumn.CurrentUser;
                updatable.InputDate = _baseInputColumn.Now;
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
