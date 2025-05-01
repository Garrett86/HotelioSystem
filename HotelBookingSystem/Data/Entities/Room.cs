using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Entities
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        [Required]
        [StringLength(50)]
        public string RoomType { get; set; }

        [Required]
        [Column(TypeName = "decimal(3,2)")]
        public decimal RoomArea { get; set; }

        [Required]
        public short MaxOccupancy { get; set; }

        [Required]
        [StringLength(50)]
        public string BedType { get; set; }

        [Required]
        [StringLength(50)]
        public string BedSize { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        // Navigation property
        public ICollection<Booking> Bookings { get; set; }
    }
}
