using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingSystem.Data.Entities
{
    [Table("CodeItem")]
    public class CodeItem
    {
        [Key]
        [StringLength(50)]
        public string CodeID { get; set; }

        public int? ItemValue { get; set; }

        [StringLength(100)]
        public string ItemText { get; set; }
    }
}
