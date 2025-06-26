using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace HotelBookingSystem.Models.DTO
{
    public class Room_Data_Search
    {
        internal object capacities;

        public string roomType { get; set; }

        public string floor { get; set; }

        public int roomNumber { get; set; }

        public string bedType { get; set; }

        public decimal price { get; set; }

        public byte capacity { get; set; }

        public string description { get; set; }

        public string facilities { get; set; }

        public string ImageURL { get; set; }

        public byte vacantRoom { get; set; }

        public string vacantRoomLabel { get; set; }

        public int cookingCount { get; set; }
        public DateTime createdAt { get; set; }

        public DateTime updatedAt { get; set; }
    }

    public class Room_Data_Table : Room_Data_Search
    {
        public int RoomId { get; set; }
    }

    public class Room_Data_Edit 
    {
        [DisplayName("使用者代碼")]
        public int RoomId { get; set; }

        [DisplayName("房型")]
        public string roomType { get; set; }

        [DisplayName("樓層")]
        public string floor { get; set; }

        [DisplayName("房間號碼")]
        public int roomNumber { get; set; }

        [DisplayName("床型")]
        public string bedType { get; set; }

        [DisplayName("價格")]
        public string price { get; set; }

        [DisplayName("人數")]
        public byte capacity { get; set; }

        public string vacantRoomLabel { get; set; }

        [DisplayName("剩餘房間")]
        public int cookingCount { get; set; }


        public string description { get; set; }

        public string facilities { get; set; }

        [DisplayName("連結")]
        public string ImageURL { get; set; }

        [DisplayName("狀　　態")]
        public byte vacantRoom { get; set; } = 0;

        public List<SelectListItem> VacantRoomOptions { get; set; }
    }
}
