using HotelBookingSystem.Models;

namespace HotelBookingSystem.Data.Repositories
{
    public interface IHotelBookingRepository<TEntity, TId>
    {
        Task<QueryResult> GetAllAsync();

        Task<QueryResult> GetByIdAsync(TId id);

        Task<QueryResult> UpdateAsync(TId id, TEntity entity);

        Task<QueryResult> AddAsync(TId id, TEntity entity);

        Task<QueryResult> DeleteAsync(TId id);
    }
}
