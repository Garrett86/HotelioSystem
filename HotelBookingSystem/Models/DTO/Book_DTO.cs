using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models.DTO
{
    public class Book_Data
    {

        public int BookingId { get; set; }

        public string UserName { get; set; }

        public int RoomId { get; set; }

        public decimal TotalAmount { get; set; }


        public DateTime CheckInDate { get; set; }


        public DateTime CheckOutDate { get; set; }


        public DateTime BookingDate { get; set; }

        public int PeopleCount { get; set; }
    }

    public class Book_Data_Search
    {
        public int BookingId { get; set; }

        public string UserName { get; set; }

        public int RoomId { get; set; }

        public decimal TotalAmount { get; set; }


        public DateTime CheckInDate { get; set; }


        public DateTime CheckOutDate { get; set; }


        public DateTime BookingDate { get; set; }
    }
}
