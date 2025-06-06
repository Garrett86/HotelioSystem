using HotelBookingSystem.Models.DTO;

namespace HotelBookingSystem.Models.ViewModel
{
    public class RoomSearchViewModel
    {
        public Member_Data_Search MemberSearch { get; set; }
        public IEnumerable<Room_Data_Table> RoomList { get; set; }
    }
}
