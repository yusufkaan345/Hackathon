using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Transportathon.Areas.Identity.Data;

namespace Transportathon.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(UserManager<ApplicationUser> userManager) : base(userManager)
        {
        }
        public async Task<IActionResult> Index()
        {
            await SetUserInfoAsync();

            return View();
        }

    }
}
