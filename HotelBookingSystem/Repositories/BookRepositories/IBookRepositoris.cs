using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DTO;

namespace HotelBookingSystem.Repositories.BookRepositories
{
    public interface IBookRepositoris
    {
        Task<int> SaveBookingAsync(Book_Data book);

        Task<IEnumerable<Book_Data_Search>> SearchBookAnync();

        Task<IEnumerable<Book_Data_Search>> SearchBookByAccountAsync(string account);
    }
}
