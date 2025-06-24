
using Dapper;
using System.Text;
using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DTO;
using HotelBookingSystem.Models.DB;

namespace HotelBookingSystem.Repositories.RoomRepositories
{
    public class RoomRepository : RepositeriesBase<Room>, IRoomRepository
    {
        public RoomRepository(HotelBookingDbContext context) : base(context)
        {
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
            SQL.AppendLine("    R.createdAt,");
            SQL.AppendLine("    R.updatedAt");
            SQL.AppendLine("FROM Rooms R");
            SQL.AppendLine("ORDER BY R.createdAt DESC");
            var parameters = new DynamicParameters();
            var whereClauses = new List<string>();
            string sql = SQL.ToString();
            var result =  await ExecuteQuery<Room_Data_Table>(sql);

            return result;
        }
    }
}
