using HotelBookingSystem.Models.DTO;
using HotelBookingSystem.Repositories.BookRepositories;

namespace HotelBookingSystem.Services.BookingService
{
    public class BookingService : IBookingService
    {
        private readonly IBookRepositoris _book;

        public BookingService(IBookRepositoris book)
        {
            _book = book ?? throw new ArgumentNullException(nameof(book));
        }

        public async Task<IEnumerable<Book_Data_Search>> BookingByaccount(string account)
        {
            var result = await _book.SearchBookByAccountAsync(account);
            return result;
        }

        public async Task<int> SaveAnync(Book_Data book_Data)
        {
            var result = await _book.SaveBookingAsync(book_Data);
            return result;
        }

    }
}
