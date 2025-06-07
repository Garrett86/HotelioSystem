using HotelBookingSystem.Services.Hotel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace HotelBookingSystem.Controllers
{
    public class LoginController : OrderBaseController
    {
        private readonly IMemberRepository _member;

        public LoginController(IMemberRepository repository)
        {
            _member = repository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string account, string password)
        {
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "帳號或密碼不能為空";
                return RedirectToAction("Index", "Home");
            }

            // 查詢使用者
            var user = await _member.GetUserByAccountAsync(account);

            if (user == null)
            {
                ViewBag.ErrorMessage = "帳號不存在";
                return RedirectToAction("Index", "Home");
            }

            if (user.password != password)
            {
                ViewBag.ErrorMessage = "密碼錯誤";
                return RedirectToAction("Index", "Home");
            }

            if (user.account == "root")
            {
                return Redirect("/api/Root/rootpage");
            }
            // 設置登入 Cookie
            Response.Cookies.Append("UserAccount", account, new CookieOptions
            {
                HttpOnly = true,
                //Expires = DateTime.Now.AddDays(7)  // 設定過期時間，例如 7 天
            });

            // 轉向成功頁面
            return RedirectToAction("BookPage", "Book");
        }




        public IActionResult Logout()
        {
            if (Request.Cookies.ContainsKey("UserAccount"))
            {
                Response.Cookies.Delete("UserAccount");
            }

            // 返回登入頁面
            return RedirectToAction("BookPage", "Book");
        }
    }
}
