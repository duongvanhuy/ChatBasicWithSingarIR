using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VFT.ChartSingalR.Areas.Identity.Data;

namespace VFT.ChartSingalR.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<VFTChartSingalRUser> SignInManager;
        private  readonly UserManager<VFTChartSingalRUser> UserManager;

        //public UserController()
        //{

        //}
        public UserController(SignInManager<VFTChartSingalRUser> SignInManager,
                            UserManager<VFTChartSingalRUser> UserManager)
        {
            this.SignInManager = SignInManager;
            this.UserManager = UserManager;

        }
        public IActionResult Index()
        {
            ViewData["userLoggin"] = User.Identity.IsAuthenticated ?
               getIdUserLoggin()
               : null;

            // lấy ra id người dùng đã đăng nhập
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // trả về danh sách người dùng khác ngoài người dùng đã đăng nhập
            var users = UserManager.Users.Where(u => u.Id != userId);

            return View(users);
        }
        public string getIdUserLoggin()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId;
        }
    }
}
