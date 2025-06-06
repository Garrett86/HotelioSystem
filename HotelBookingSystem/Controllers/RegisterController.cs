using HotelBookingSystem.Data;
using HotelBookingSystem.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Controllers
{
    public class RegisterController : OrderBaseController
    {
        private readonly HotelBookingDbContext _context;

        public RegisterController(HotelBookingDbContext context)
        {
            _context = context;
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

            member.memberid = GenerateNextMember();
            //設定註冊日期
            member.initDate = DateTime.Now;

            // 儲存到資料庫
            _context.Members.Add(member);
            _context.SaveChanges();

            // 顯示註冊成功訊息
            TempData["SucessMessage"] = $"{member.account} 註冊成功! 請登入";

            return RedirectToAction("BookPage", "Book");
        }


        private string GenerateNextMember()
        {
            var maxMemberId = _context.Members
                .OrderByDescending(m => m.memberid)
                .Select(m => m.memberid)
                .FirstOrDefault();

            int nexNumber = 1;
            if (!string.IsNullOrEmpty(maxMemberId) && maxMemberId.Length == 4 && maxMemberId.StartsWith("M"))
            {
                if (int.TryParse(maxMemberId.Substring(1), out int currentNumber))
                {
                    nexNumber = currentNumber + 1;
                }
            }

            return "M" + nexNumber.ToString("D3");
        }
    }
}
