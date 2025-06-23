using System.ComponentModel.DataAnnotations;
using System.Reflection;

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

    /// <summary>
    /// 擴充方法：抓取 Enum 的 Display 名稱
    /// </summary>
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var member = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();
            var display = member?.GetCustomAttribute<DisplayAttribute>();
            return display?.Name ?? enumValue.ToString();
        }
    }

}
