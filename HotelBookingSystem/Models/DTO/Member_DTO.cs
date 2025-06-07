using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingSystem.Models.DTO
{
   public class Member_Data_Search
    {

        [DisplayName("使用者帳號")]
        public string account { get; set; }

        [DisplayName("密碼")]
        public string password { get; set; }

        [DisplayName("姓")]
        public string lastName { get; set; }

        [DisplayName("名")]
        public string firstName { get; set; }

        [DisplayName("身分證號碼")]
        public string cardID { get; set; }

        [DisplayName("電子信箱")]
        public string email { get; set; }

        [DisplayName("身分證號碼")]
        public string phone { get; set; }


        [DisplayName("生日")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? birthday { get; set; }

        [DisplayName("性別")]
        public byte? gender { get; set; }

        [DisplayName("國籍")]
        public string nationality { get; set; }

        [DisplayName("城市")]
        public string city { get; set; }

    }

    public class Member_Data_Edit:Member_Data_Search
    {
        [DisplayName("使用者名稱")]
        public string memberid { get; set; }

        [DisplayName("密碼確認")]
        public string passwordConfirm { get; set; }

        [NotMapped]
        public List<SelectListItem> NationalityOptions { get; set; } = new List<SelectListItem>
        {
        new SelectListItem { Value = "台灣", Text = "台灣" },
        new SelectListItem { Value = "日本", Text = "日本" },
        new SelectListItem { Value = "韓國", Text = "韓國" },
        };

        [NotMapped]
        public List<SelectListItem> CityOptions { get; set; } = new List<SelectListItem>
        {
        new SelectListItem { Value = "臺北市", Text = "臺北市" },
        new SelectListItem { Value = "台中市", Text = "台中市" },
        new SelectListItem { Value = "高雄市", Text = "高雄市" },
        };
    }
  
}
