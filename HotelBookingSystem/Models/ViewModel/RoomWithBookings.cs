using HotelBookingSystem.Models.DTO;

namespace HotelBookingSystem.Models.ViewModel
{
    public class RoomWithBookings
    {
        public decimal price { get; set; }
        public List<Book_Data_Search> Bookings { get; set; } = new();
    }
}
