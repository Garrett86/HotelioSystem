using HotelBookingSystem.Models.DB;

namespace HotelBookingSystem.Data.Repositories
{
    public class HotelBookingRepository<TEntity, TId> : BaseRepository<TEntity, TId>, IHotelBookingRepository<TEntity, TId> where TEntity : class
    {
        public HotelBookingRepository(HotelBookingDbContext hotelBookingDbContext, ILoggerFactory loggerFactory) 
        : base(hotelBookingDbContext, loggerFactory)
        {
        }
    }
}
