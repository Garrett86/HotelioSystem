using HotelBookingSystem.Models.DTO;

namespace HotelBookingSystem.Models.ViewModel
{
    public class RoomSearchViewModel
    {
        /// <summary>
        /// page
        /// </summary>
        public int? vacantRoom {  get; set; }
        public int Page { get; set; }

        public int PageSize { get; set; }
        public IEnumerable<Room> Items { get; set; }
    }
}
