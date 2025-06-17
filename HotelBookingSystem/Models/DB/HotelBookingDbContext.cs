using HotelBookingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingSystem.Models.DB
{
    public class HotelBookingDbContext : DbContext
    {

        //初始化資料庫上下文
        //public HotelBookingDbContext(DbContextOptions<HotelBookingDbContext> options): base(options){ }
        public HotelBookingDbContext(DbContextOptions<HotelBookingDbContext> options): base(options){ }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Booking { get; set; }


        public DbSet<Member> Members { get; set; }

        public DbSet<CodeItem> CodeItems { get; set; }
    }
}
