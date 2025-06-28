
using Dapper;
using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DB;
using HotelBookingSystem.Models.DTO;
using Microsoft.Data.SqlClient;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HotelBookingSystem.Repositories.RoomRepositories
{
    public class RoomRepository : RepositeriesBase<Room>, IRoomRepository
    {

        private readonly string _connString;
        public RoomRepository(HotelBookingDbContext context, IConfiguration config) : base(context, config)
        {
            _connString = config.GetConnectionString("HotelBookingConnection");
        }

        public async Task<IEnumerable<Room_Data_Table>> SearchRoomByIdAnync(int id)
        {
            var sql = @"SELECT * FROM  Rooms WHERE roomId = @id";
            var result = await ExecuteQuery<Room_Data_Table>(sql, new { id });
            return result;
        }

        public async Task<IEnumerable<Room_Data_Table>> SearchRooms()
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
            SQL.AppendLine("    Ci.ItemText AS vacantRoomLabel,");
            SQL.AppendLine("    R.cookingCount,");
            SQL.AppendLine("    R.createdAt,");
            SQL.AppendLine("    R.updatedAt");
            SQL.AppendLine("FROM Rooms R");
            SQL.AppendLine("LEFT JOIN CodeItem Ci ON R.vacantRoom = Ci.ItemValue AND Ci.CodeID = 'vacantRoom'");
            SQL.AppendLine("ORDER BY R.createdAt DESC");

            var parameters = new DynamicParameters();
            var whereClauses = new List<string>();
            string sql = SQL.ToString();
            var result = await ExecuteQuery<Room_Data_Table>(sql);

            return result;
        }

        public async Task UpdateRoomByVacantRoomAnync(int id, int vacantRoom)
        {

            var sql = @"UPDATE Rooms SET vacantRoom = @vacantRoom WHERE roomId = @id";

            await this.ExecuteNonQueryAsync(sql, new { vacantRoom, id });

        }
    }
}
