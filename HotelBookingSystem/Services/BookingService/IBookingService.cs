using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DTO;

namespace HotelBookingSystem.Services.BookingService
{
    public interface IBookingService
    {
        Task<int> SaveAnync(Book_Data book_Data);

        Task<IEnumerable<Book_Data_Search>> BookingByaccount(string account);

        Task<Book_Data_Search> GetBookByAccountOnShoping(string account);

        Task<Room_Data_Table> GetBookByNewData(string account);
        Task<IEnumerable<Room_Data_Table>> GetBookByNewDatas(string account);

        Task<int> DeleteBookingByRoomId(int id);
    }
}
