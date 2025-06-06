namespace HotelBookingSystem.Models.DTO
{
    public class Room_Data_Search
    {

        public string roomType { get; set; }

        public string floor { get; set; }

        public int roomNumber { get; set; }

        public string bedType { get; set; }

        public string price { get; set; }

        public byte capacity { get; set; }

        public string description { get; set; }

        public string facilities { get; set; }

        public string ImageURL { get; set; }

        public byte vacantRoom { get; set; }

        public DateTime createdAt { get; set; }

        public DateTime updatedAt { get; set; }
    }

    public class Room_Data_Table : Room_Data_Search
    {
        public int RoomId { get; set; }
    }
}
