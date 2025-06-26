using AutoMapper;
using Dapper;
using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DB;
using HotelBookingSystem.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace HotelBookingSystem.Repositories.BookRepositories
{
    public class BookRepositoris :RepositeriesBase<Booking>,IBookRepositoris
    {
        private readonly IMapper _mapper;
        private readonly string _connString;

        public BookRepositoris(HotelBookingDbContext context, IMapper mapper, IConfiguration config) : base(context, config)
        {
            _mapper = mapper;
            _connString = config.GetConnectionString("HotelBookingConnection");
        }

        public async Task<int> SaveBookingAsync(Book_Data book)
        {
            var sql = @"
        INSERT INTO Booking 
        (bookingId, userName, roomId, checkInDate, checkOutDate, bookingDate, totalAmount)
        VALUES 
        (@bookingId, @userName, @roomId, @checkInDate, @checkOutDate, @bookingDate, @totalAmount)";
            try
            {
                using var conn = new SqlConnection(_connString);
                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                var result = await conn.ExecuteAsync(sql, book); // 注意是 ExecuteAsync

                return result; // 回傳受影響列數（int）
            }
            catch (Exception ex)
            {
                Console.WriteLine("查詢失敗：" + ex.ToString());
                throw new ApplicationException("資料庫查詢失敗", ex);
            }
        }

        public async Task<IEnumerable<Book_Data_Search>> SearchBookAnync()
        {
            var sql = "SELECT * FROM Booking";
            var result= await ExecuteQuery<Book_Data_Search>(sql);
            return result;
        }

public async Task<IEnumerable<Book_Data_Search>> SearchBookByAccountAsync(string account)
{
    var sql = "SELECT * FROM Booking WHERE userName = @userName";  // 修正這裡，移除空白
    var result = await ExecuteQuery<Book_Data_Search>(sql, new { userName = account });
    return result;
}
    }
}
