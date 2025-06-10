using HotelBookingSystem.Models.DTO;
using Microsoft.AspNetCore.Authorization;

namespace HotelBookingSystem.ActionFilter
{
    //AuthorizeAttribute 最早執行 權限檢查必須最早
    public class CheckPointAttribute :AuthorizeAttribute
    {
        public string Menu_Url { get; set; }
        public string SPName { get; set; }

        public PermissionType Permission { get; set; }

        public CheckPointAttribute(PermissionType permission)
        {
            this.Permission = permission;
        }
    }
}
