using HotelBookingSystem.Models.DTO;

namespace HotelBookingSystem.Models.ViewModel
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<Book_Data> BookingData { get; set; }
        public IEnumerable<Room_Data_Table> RoomData { get; set; }
    }
}
