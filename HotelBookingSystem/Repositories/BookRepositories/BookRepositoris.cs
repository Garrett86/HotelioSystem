using AutoMapper;
using Dapper;
using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DB;
using HotelBookingSystem.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HotelBookingSystem.Repositories.BookRepositories
{
    public class BookRepositoris : RepositeriesBase<Booking>, IBookRepositoris
    {
        private readonly IMapper _mapper;

        public BookRepositoris(HotelBookingDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<int> SaveBookingAsync(Book_Data book)
        {
            var sql = @"
        INSERT INTO Booking 
        (bookingId, userName, roomId, checkInDate, checkOutDate, bookingDate, totalAmount)
        VALUES 
        (@bookingId, @userName, @roomId, @checkInDate, @checkOutDate, @bookingDate, @totalAmount)";
            using var conn = db.Database.GetDbConnection(); // 你若有封裝，則用那個方法取代
            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            var result = await conn.ExecuteAsync(sql, book); // Dapper 的 ExecuteAsync

            return result; // 回傳受影響列數（int）
        }

        public async Task<IEnumerable<Book_Data_Search>> SearchBookAnync()
        {
            //var sql = "SELECT * FROM Booking";
            var sql = "SELECT * FROM \"Booking\";";
            var result = await ExecuteQuery<Book_Data_Search>(sql);
            return result;
        }

        public async Task<IEnumerable<Book_Data_Search>> SearchBookByAccountAsync(string account)
        {
            //var sql = "SELECT * FROM Booking WHERE userName = @userName";  // 修正這裡，移除空白
            var sql = "SELECT * FROM \"Booking\" WHERE \"userName\" = @userName"; // 修正這裡，移除空白
            var result = await ExecuteQuery<Book_Data_Search>(sql, new { userName = account });
            return result;
        }
    }
}
