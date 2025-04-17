using Cms.Attributes;
using Cms.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Cms.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserInfoService userInfoService;
        public LoginController(IUserInfoService userInfoService)
        {
            this.userInfoService = userInfoService;
        }
        [NoCheck]
        public async Task<IActionResult> Index()
        {
            if (!string.IsNullOrEmpty(Request.Query["returnUrl"]))//如果返回的地址非空
            {
                ViewBag.Url = Request.Query["returnUrl"];
            }
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("user")))//如果在session中有数值
            {
                return Redirect("/home");
            }
            if (!string.IsNullOrEmpty(HttpContext.Request.Cookies["userId"]))//如果cookie非空
            {
                int userId = Convert.ToInt32(HttpContext.Request.Cookies["userId"]);
                var user = await userInfoService.LoadEntities(u => u.Id == userId).FirstOrDefaultAsync();
                if (user != null)
                {
                    HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));
                    return Redirect("/home/index");
                }
            }
            return View();
        }
        [NoCheck]
        [HttpPost]
        public async Task<IActionResult> UserLogin()
        {
            string userName = Request.Form["txtName"]!;
            string userPwd = Request.Form["txtPwd"]!;
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userPwd))
            {
                return Json(new { code = 501, message = "用户名或密码不能为空" });
            }
            var user = await userInfoService.LoadEntities(u => u.UserName == userName && u.UserPassword == userPwd).FirstOrDefaultAsync();
            if (user == null)
            {
                return Json(new { code = 502, message = "用户名或密码错误" });
            }
            // 判断用户是否选中了免登录
            if (!string.IsNullOrEmpty(Request.Form["checkMe"]))
            {
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddDays(10);
                HttpContext.Response.Cookies.Append("userId", user.Id.ToString(), cookieOptions);
            }
            HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));

            return Json(new { code = 200, message = "登录成功" });

        }
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("userId");
            HttpContext.Session.Remove("user");
            return Json(new { code = 200, message = "退出成功" });
        }
    }
}
