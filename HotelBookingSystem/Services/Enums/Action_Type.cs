using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Services.Enums
{
    public enum Action_Type
    {
        [Display(Name = "新增")]
        Insert,
        [Display(Name = "修改")]
        Update,
        [Display(Name = "刪除")]
        Delete,
    }
}
