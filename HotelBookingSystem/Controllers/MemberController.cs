using AutoMapper;
using HotelBookingSystem.Models.DTO;
using HotelBookingSystem.Services.Hotel;
using HotelBookingSystem.Services.TextFileLogger;
using Microsoft.AspNetCore.Mvc;


namespace HotelBookingSystem.Controllers
{
    public class MemberController : OrderBaseController
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;
        private readonly ITextFileLogger _textFileLogger;
        public MemberController(IMemberRepository repository, IMapper mapper, ITextFileLogger textFileLogger)
        {
            _memberRepository = repository;
            _mapper = mapper;
            _textFileLogger = textFileLogger;
        }
        public async Task<IActionResult> Member()
        {
            Member_Data_Edit member = null;
            if (Request.Cookies.ContainsKey("UserAccount"))
            {
                string userAccount = Request.Cookies["UserAccount"];
                var memberEntity = await _memberRepository.GetUserByAccountAsync(userAccount);
                if (memberEntity != null)
                {
                    try
                    {

                        // 使用 AutoMapper 將 Entity 映射成 DTO
                        member = _mapper.Map<Member_Data_Edit>(memberEntity);
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
                var member = await _memberRepository.SaveMemberAsync(Member_Edit);

                if (Request.Cookies.ContainsKey("UserAccount"))
                {
                    Response.Cookies.Delete("UserAccount");
                }
             
                return RedirectToAction("Index", "Home");
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
        public IActionResult Order()
        {
            return View();
        }
    }
}
