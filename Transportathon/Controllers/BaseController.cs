using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Transportathon.Areas.Identity.Data;

namespace Transportathon.Controllers
{
    public class BaseController : Controller
    {
        protected readonly UserManager<ApplicationUser> _userManager;
        protected ApplicationUser _user;
        public BaseController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task SetUserInfoAsync()
        {
           _user = await _userManager.GetUserAsync(User);

            if (_user != null)
            {
                var userRole = _user.Role;
                var email = _user.Email;
                ViewBag.UserRole = userRole;
                ViewBag.Email = email;
            }
        }
    }

}
