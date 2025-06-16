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

        protected JsonResult CustomJsonResult(bool isSuccess, string message = "", Dictionary<string, string[]> modelStateErrors = null)
        {
            string customMessage = string.Empty;



            customMessage = isSuccess ? "儲存成功" : "儲存失敗";

            if (message != "") customMessage = message; // 如果不是預設值就帶客製化的

            return Json(new { IsSuccess = isSuccess, Message = customMessage, ModelStateErrors = modelStateErrors });
        }
    }
}
