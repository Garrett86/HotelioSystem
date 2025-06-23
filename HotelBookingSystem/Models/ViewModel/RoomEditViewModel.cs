namespace HotelBookingSystem.Models.ViewModel
{
    public class RoomEidtViewModel
    {
        public bool IsSuccess { get; set; }
        public string message { get; set; } = string.Empty;
        public IEnumerable<Room> Item { get; set; } = Enumerable.Empty<Room>();
    }
}
