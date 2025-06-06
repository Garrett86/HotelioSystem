using Dapper;
using HotelBookingSystem.Data;
using HotelBookingSystem.Data.Entities;
using HotelBookingSystem.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace HotelBookingSystem.Services.RoomService
{
    public class RoomService : ServiceBase<Room>, IRoomRepository
    {
        public RoomService(HotelBookingDbContext context) : base(context)
        {

        }


        public async Task<IEnumerable<Room_Data_Table>> SearchRooms(Room_Data_Search Room_Search)
        {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT");
            SQL.AppendLine("    R.roomId,");
            SQL.AppendLine("    R.roomType,");
            SQL.AppendLine("    R.floor,");
            SQL.AppendLine("    R.roomNumber,");
            SQL.AppendLine("    R.bedType,");
            SQL.AppendLine("    R.price,");
            SQL.AppendLine("    R.capacity,");
            SQL.AppendLine("    R.description,");
            SQL.AppendLine("    R.facilities,");
            SQL.AppendLine("    R.ImageURL,");
            SQL.AppendLine("    R.vacantRoom,");
            SQL.AppendLine("    R.createdAt,");
            SQL.AppendLine("    R.updatedAt");
            SQL.AppendLine("FROM Rooms R");
            var parameters = new DynamicParameters();
            var whereClauses = new List<string>();



            SQL.AppendLine("ORDER BY R.createdAt DESC");
            string sql = SQL.ToString();
            var result = ExecuteQuery<Room_Data_Table>(sql);
            return result;
        }
    }
}
