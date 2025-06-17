using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models.DTO
{
    public class Book_Data
    {

        public int bookingId { get; set; }

        public string userName { get; set; }

        public int roomId { get; set; }

        public decimal totalAmount { get; set; }


        public DateTime CheckInDate { get; set; }


        public DateTime CheckOutDate { get; set; }


        public DateTime BookingDate { get; set; }

        public int PeopleCount { get; set; }
    }

    public class Book_Data_Search
    {
        public int bookingId { get; set; }

        public string userName { get; set; }

        public int roomId { get; set; }

        public decimal totalAmount { get; set; }


        public DateTime checkInDate { get; set; }


        public DateTime checkOutDate { get; set; }


        public DateTime bookingDate { get; set; }
    }
}
