using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingSystem.Models
{
    public class Member
    {
        [Key]
        public string? memberid { get; set; }

        [Required(ErrorMessage = "帳號為必填項目")]
        public string account { get; set; }
        [Required(ErrorMessage = "密碼為必填項目")]
        public string password { get; set; }

        [Required(ErrorMessage = "姓為必填項目")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "名為必填項目")]
        public string lastName { get; set; }

        public string? cardID { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public DateTime birthday { get; set; }
        public byte? gender { get; set; }
        public string? nationality { get; set; }

        public string? city { get; set; }

        public DateTime initDate { get; set; }

        [NotMapped]
        [Compare("password", ErrorMessage = "密碼與確定密碼不一樣")]
        public string passwordConfirm { get; set; }
    }
}
