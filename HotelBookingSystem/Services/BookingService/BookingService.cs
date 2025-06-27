using AutoMapper;
using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DB;
using HotelBookingSystem.Models.DTO;
using HotelBookingSystem.Repositories.BookRepositories;
using HotelBookingSystem.Repositories.RoomRepositories;
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

        public async Task<int> DeleteBookingByRoomId(int id)
        {
            var bookings = await _db.Booking
                .Where(b => b.RoomId == id)
                .ToListAsync();

            if (!bookings.Any())
                return 0;

            _db.Booking.RemoveRange(bookings);
            return await _db.SaveChangesAsync();
        }

        public async Task<Book_Data_Search> GetBookByAccountOnShoping(string account)
        {
            var result = await BookingByaccount(account);
            var latest = result.MaxBy(x => x.initDate);
            return latest;
        }

        public async Task<Room_Data_Table> GetBookByNewData(string account)
        {
            var result = await this._book.SearchBookByAccountAsync(account);

            //var latest = result?.OrderByDescending(x => x.initDate).FirstOrDefault();
            var latest = result.MaxBy(x => x.initDate);  // .NET 6+ 可用

            if (latest == null)
                return null;

            var datas = await this._book.GetRoomWithBookingsAsync(latest.bookingId);
            var data = datas.FirstOrDefault();
            return data;
        }

        public async Task<IEnumerable<Room_Data_Table>> GetBookByNewDatas(string account)
        {
           var bookings = await this.BookingByaccount(account);
            var roomIds = bookings.Select(b => b.bookingId).Distinct();
            var result = await this._book.GetRoomWithBookingsByIdsAsync(roomIds);
            return result;
        }

        public async Task<int> SaveAnync(Book_Data book_Data)
        {
            var result = await _book.SaveBookingAsync(book_Data);
            return result;
        }



    }
}
