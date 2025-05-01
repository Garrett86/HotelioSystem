using HotelBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Data.Repositories
{
    public class BaseRepository<TEntity, TId> where TEntity : class
    {
        private readonly DbContext _dbContext;
        private readonly ILogger _logger;

        public BaseRepository(DbContext dbContext, ILoggerFactory loggerFactory)
        {
            _dbContext = dbContext;
            _logger = loggerFactory.CreateLogger(typeof(TEntity).Name);
        }

        public async Task<QueryResult> GetAllAsync() =>
            await RunSafeAsync(() => _dbContext.Set<TEntity>().ToListAsync());    // 找不到回傳 new List<entity>()

        public async Task<QueryResult> GetByIdAsync(TId id) =>
            await RunSafeAsync(() => _dbContext.Set<TEntity>().FindAsync(id).AsTask());    // FindAsync 會回傳 ValueTask 所以必須 .AsTask()

        public async Task<QueryResult> UpdateAsync(TId id, TEntity entity)
        {
            return await RunSafeAsync(async () =>
            {
                var existingEntity = await _dbContext.Set<TEntity>().FindAsync(id);
                if (existingEntity is null) throw new Exception("資料不存在");

                _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
                return await _dbContext.SaveChangesAsync();    // 回傳影響筆數
            });
        }

        public async Task<QueryResult> AddAsync(TId id, TEntity entity)
        {
            return await RunSafeAsync(async () =>
            {
                if (entity is null) throw new Exception("輸入資料錯誤");

                var existingEntity = await _dbContext.Set<TEntity>().FindAsync(id);    // 檢查是否存在
                if (existingEntity is not null) throw new Exception("資料已存在");

                await _dbContext.Set<TEntity>().AddAsync(entity);
                return await _dbContext.SaveChangesAsync();
            });
        }

        public async Task<QueryResult> DeleteAsync(TId id)
        {
            return await RunSafeAsync(async () =>
            {
                var existingEntity = await _dbContext.Set<TEntity>().FindAsync(id);
                if (existingEntity is null) throw new Exception("資料不存在");

                _dbContext.Remove(existingEntity);
                return await _dbContext.SaveChangesAsync();
            });
        }

        // 統一例外處理，出錯回傳設定值，方便控制層管理，並記錄錯誤訊息
        private async Task<QueryResult> RunSafeAsync<T>(Func<Task<T>> action)
        {
            try
            {
                var resultData = await action();
                return new QueryResult
                {
                    Data = resultData,
                    ErrorMessage = string.Empty
                };

            }
            catch (Exception ex)
            {
                var currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                _logger.LogError(ex, "{class} 資料庫錯誤發生於: {Time}", this.GetType().Name, currentTime);
                return new QueryResult
                {
                    Data = default!,
                    ErrorMessage = ex.ToString()
                };
            }
        }
    }
}
