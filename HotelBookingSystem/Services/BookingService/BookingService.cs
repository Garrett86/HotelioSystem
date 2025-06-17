using AutoMapper;
using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DB;
using HotelBookingSystem.Models.DTO;
using HotelBookingSystem.Repositories.BookRepositories;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Services.BookingService
{
    public class BookingService : IBookingService
    {
        private readonly IBookRepositoris _book;
        private readonly IMapper _mapper;
        private HotelBookingDbContext _db;

        public BookingService(HotelBookingDbContext context, IBookRepositoris book, IMapper mapper)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
            _book = book ?? throw new ArgumentNullException(nameof(book));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<Book_Data_Search>> BookingByaccount(string account)
        {
            var result = await _book.SearchBookByAccountAsync(account);
            return result;
        }

        public async Task<int> DeleteBookingByName(string name)
        {
            var bookings = await _db.Booking
                .Where(b => b.userName == name)
                .ToListAsync();

            if (!bookings.Any())
                return 0;

            _db.Booking.RemoveRange(bookings);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> SaveAnync(Book_Data book_Data)
        {
            var result = await _book.SaveBookingAsync(book_Data);
            return result;
        }



    }
}
