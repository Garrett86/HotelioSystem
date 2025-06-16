using AutoMapper;
using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DTO;
using HotelBookingSystem.Services.BookingService;
using HotelBookingSystem.Services.CodeItemService;
using HotelBookingSystem.Services.MemberService;
using HotelBookingSystem.Services.TextFileLogger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Action_Type = HotelBookingSystem.Services.Enums.Action_Type;


namespace HotelBookingSystem.Controllers
{
    public class MemberController : OrderBaseController
    {
        private readonly IMemberService _memberService;
        private readonly IMapper _mapper;
        private readonly ITextFileLogger _textFileLogger;
        private readonly ICodeItemService _codeItem;
        private readonly IConfiguration _configuration;
        private readonly IBookingService _booking;
        public MemberController(IMemberService memberService, IMapper mapper, ITextFileLogger textFileLogger,
            ICodeItemService codeItem,IBookingService booking, IConfiguration configuration)
        {
            _memberService = memberService;
            _mapper = mapper;
            _textFileLogger = textFileLogger;
            _codeItem = codeItem;
            _configuration = configuration;
            _booking = booking;
        }
        public async Task<IActionResult> Member()
        {
            Member_Data_Edit member = null;
            if (Request.Cookies.ContainsKey("UserAccount"))
            {
                string userAccount = Request.Cookies["UserAccount"];
                var memberEntity = await _memberService.GetUserByAccountAsync(userAccount);
                if (memberEntity != null)
                {
                    try
                    {

                        // 使用 AutoMapper 將 Entity 映射成 DTO
                        member = _mapper.Map<Member_Data_Edit>(memberEntity);
                        var item = await _codeItem.GetCodeItemById();
                        ViewData["codeItem"] = item;
                    }
                    catch (AutoMapperMappingException ex)
                    {
                        await _textFileLogger.LogAsync($"UserAccount: {userAccount}\n錯誤訊息: {ex.Message}\n堆疊追蹤: {ex.StackTrace}");
                        TempData["ErrorMessage"] = "系統發生錯誤，請稍後再試。";
                        return RedirectToAction("BookPage", "Book");
                    }
                }
            }
            if (member == null)
            {
                TempData["ErrorMessage"] = "請先登入";
                return RedirectToAction("BookPage", "Book");
            }
            return View("Member", member);
        }

        [HttpPost]
        public async Task<IActionResult> SaveModifiedMember(Member_Data_Edit Member_Edit)
        {
            try
            {
                var member = await _memberService.SaveMemberAsync(Member_Edit);

                if (Request.Cookies.ContainsKey("UserAccount"))
                {
                    Response.Cookies.Delete("UserAccount");
                }

                return RedirectToAction("BookPage","Book");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("MemberModify", Member_Edit);
            }
        }

        public IActionResult Bonding()
        {
            return View();
        }
        public async Task<IActionResult> Order()
        {
            var user = Request.Cookies["UserAccount"];
            var bookingdata = await _booking.BookingByaccount(user);
            ViewData["BookData"] = bookingdata;
            return View();
        }

        private ActionResult Save(Member_Data_Edit member_Data_Edit, Action_Type eAction_Type)
        {
            string sMessage ="";
            var liError = new List<string>();
            bool isSuccess = false;

            if (eAction_Type == Action_Type.Insert && !member_Data_Edit.memberid.IsNullOrEmpty())
            {
                if (_memberService.IsMemberIDExist(member_Data_Edit.memberid))
                {
                    liError.Add("使用者帳戶" + member_Data_Edit.memberid + "已存在");
                }
            }

            if (!liError.Any())
            {
                try
                {
                    var Member_Data = _memberService.GetDataOrDefaultByPKey(member_Data_Edit.memberid);

                    switch (eAction_Type)
                    {
                        case Action_Type.Insert:
                        case Action_Type.Update:
                            Member_Data = _mapper.Map<Member>(member_Data_Edit);
                            break;
                    }
                    _memberService.Update(Member_Data, eAction_Type);
                    isSuccess = true;

                }catch(Exception ex)
                {
                    string logPath = _configuration["Logging:ErrLogPath"];
                    _textFileLogger.LogAsync(logPath, ex.ToString());

                    sMessage = "儲存失敗，請洽系統管理員";
                }
            }
            else
            {
                sMessage += string.Join("\n", liError);
            }
            return CustomJsonResult(isSuccess, sMessage);
        }

    }
}
