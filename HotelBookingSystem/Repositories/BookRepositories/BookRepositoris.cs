using AutoMapper;
using Dapper;
using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DB;
using HotelBookingSystem.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;

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

        public async Task<IEnumerable<Room_Data_Table>> GetRoomWithBookingsAsync(int id)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("    R.[roomId],");
            sql.AppendLine("    R.[roomType],");
            sql.AppendLine("    R.[floor],");
            sql.AppendLine("    R.[roomNumber],");
            sql.AppendLine("    R.[bedType],");
            sql.AppendLine("    R.[price],");
            sql.AppendLine("    R.[capacity],");
            sql.AppendLine("    R.[description],");
            sql.AppendLine("    R.[facilities],");
            sql.AppendLine("    R.[ImageURL],");
            sql.AppendLine("    R.[vacantRoom],");
            sql.AppendLine("    R.[cookingCount],");
            sql.AppendLine("    R.[createdAt],");
            sql.AppendLine("    R.[updatedAt],");
            sql.AppendLine("    B.[bookingId],");
            sql.AppendLine("    B.[userName],");
            sql.AppendLine("    B.[initDate]");
            sql.AppendLine("FROM [HotelBookingSystem].[dbo].[Rooms] R");
            sql.AppendLine("LEFT JOIN [Booking] B ON R.roomId = B.roomId");
            sql.AppendLine("WHERE R.roomId = @id");
            sql.AppendLine("ORDER BY B.initDate DESC;");

            var result = await ExecuteQuery<Room_Data_Table>(sql.ToString(), new { id });
            return result;
        }

        public async Task<IEnumerable<Room_Data_Table>> GetRoomWithBookingsByIdsAsync(IEnumerable<int> ids)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("    R.[roomId],");
            sql.AppendLine("    R.[roomType],");
            sql.AppendLine("    R.[floor],");
            sql.AppendLine("    R.[roomNumber],");
            sql.AppendLine("    R.[bedType],");
            sql.AppendLine("    R.[price],");
            sql.AppendLine("    R.[capacity],");
            sql.AppendLine("    R.[description],");
            sql.AppendLine("    R.[facilities],");
            sql.AppendLine("    R.[ImageURL],");
            sql.AppendLine("    R.[vacantRoom],");
            sql.AppendLine("    R.[cookingCount],");
            sql.AppendLine("    R.[createdAt],");
            sql.AppendLine("    R.[updatedAt],");
            sql.AppendLine("    B.[bookingId],");
            sql.AppendLine("    B.[userName],");
            sql.AppendLine("    B.[initDate]");
            sql.AppendLine("FROM [HotelBookingSystem].[dbo].[Rooms] R");
            sql.AppendLine("LEFT JOIN [Booking] B ON R.roomId = B.roomId");
            sql.AppendLine("WHERE R.roomId  IN @ids");
            sql.AppendLine("ORDER BY B.initDate DESC;");

            var result = await ExecuteQuery<Room_Data_Table>(sql.ToString(), new { ids });
            return result;
        }

        public async Task<int> SaveBookingAsync(Book_Data book)
        {
            var sql = @"
        INSERT INTO Booking 
        (bookingId, userName, roomId, PeopleCount, totalAmount,checkInDate, checkOutDate, bookingDate)
        VALUES 
        (@bookingId, @userName, @roomId,@PeopleCount,@totalAmount,@checkInDate, @checkOutDate, @bookingDate)";
            var result = await this.ExecuteNonQueryAsync(sql, book);
            return result; 
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
