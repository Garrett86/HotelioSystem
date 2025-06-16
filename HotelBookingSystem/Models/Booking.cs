using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        public int RoomId { get; set; }

        
        public DateTime CheckInDate { get; set; }

        
        public DateTime CheckOutDate { get; set; }

        
        public DateTime BookingDate { get; set; }

        
        public decimal TotalAmount { get; set; }

        public Room Room { get; set; }
        public Customer Customer { get; set; }
    }
}
