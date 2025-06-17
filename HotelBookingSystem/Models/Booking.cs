using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models
{
    [Table("Booking")]
    public class Booking
    {
        [Key]
        public int bookingId { get; set; }

        [Required]
        [StringLength(50)]
        public string userName { get; set; }

        [Required]
        public int RoomId { get; set; }

        
        public DateTime checkInDate { get; set; }

        
        public DateTime checkOutDate { get; set; }

        
        public DateTime bookingDate { get; set; }

        
        public decimal totalAmount { get; set; }

        public Room Room { get; set; }

        [ForeignKey("userName")]
        public Customer Customer { get; set; }
    }
}
