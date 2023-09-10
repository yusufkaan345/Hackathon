using Microsoft.AspNetCore.Mvc;

namespace Transportathon.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult LogIn()
        {
            return View();
        }
    }
}
