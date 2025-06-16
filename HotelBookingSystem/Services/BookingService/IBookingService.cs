using HotelBookingSystem.Models.DTO;

namespace HotelBookingSystem.Services.BookingService
{
    public interface IBookingService
    {
        Task<int> SaveAnync(Book_Data book_Data);

        Task<IEnumerable<Book_Data_Search>> BookingByaccount(string account);
    }
}
