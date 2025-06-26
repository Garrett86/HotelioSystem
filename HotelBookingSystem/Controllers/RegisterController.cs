using HotelBookingSystem.Data.Entities;
using HotelBookingSystem.Models;
using HotelBookingSystem.Models.DB;
using HotelBookingSystem.Services.MemberService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Controllers
{
    public class RegisterController : OrderBaseController
    {
        private readonly HotelBookingDbContext _context;
        private readonly IMemberService _member;

        public RegisterController(HotelBookingDbContext context, IMemberService memberService)
        {
            _context = context;
            _member = memberService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Member member)
        {
            // 先前有登入, 先強制 Logout
            if (Request.Cookies.ContainsKey("UserAccount"))
            {
                Response.Cookies.Delete("UserAccount");
            }

            if (!ModelState.IsValid)
            {
                return View(member);
            }

            // 檢查帳號是否存在
            if (_context.Members.Any(m => m.account == member.account))
            {
                ModelState.AddModelError("account", "帳號已存在，請輸入其他帳號");
                return View(member);
            }

            member.memberid = this._member.GenerateNextMember();
            //設定註冊日期
            member.initDate = DateTime.Now;

            // 儲存到資料庫
            _context.Members.Add(member);
            _context.SaveChanges();

            // 顯示註冊成功訊息
            TempData["SucessMessage"] = $"{member.account} 註冊成功! 請登入";

            return RedirectToAction("BookPage", "Book");
        }

    }
}
