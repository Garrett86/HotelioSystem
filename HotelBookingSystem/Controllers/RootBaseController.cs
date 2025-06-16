using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HotelBookingSystem.Controllers
{
    public class RootBaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewData["Layout"] = "~/Views/Shared/_RootLayout.cshtml";
            base.OnActionExecuting(context);
        }
    }
}
