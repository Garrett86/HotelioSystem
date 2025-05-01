using HotelBookingSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Data
{
    public class HotelBookingDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        //初始化資料庫上下文
        public HotelBookingDbContext(DbContextOptions<HotelBookingDbContext> options)
        : base(options)
        {
        }
    }
}
