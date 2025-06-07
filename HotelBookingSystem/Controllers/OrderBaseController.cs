using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HotelBookingSystem.Controllers
{
    public class OrderBaseController : Controller
    {
       public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewData["Layout"] = "~/Views/Shared/_OrderLayout.cshtml";
            base.OnActionExecuting(context);
        }
    }
}
