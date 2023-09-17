using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Transportathon.Areas.Identity.Data;
using Transportathon.Data;
using Transportathon.Models;

namespace Transportathon.Controllers
{
    public class MessagesController : BaseController
    {
        private readonly AppDbContext _dbContext;

        public MessagesController(UserManager<ApplicationUser> userManager, AppDbContext dbContext) : base(userManager)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Messageform(string userid, string userName)
        {

            await SetUserInfoAsync();

            var model = new CreateMessage();
            var user = await _userManager.FindByIdAsync(_user.Id);

            if (user.Role == "customer")
            {
                
                ViewBag.DriverId = userid;
                ViewBag.DriverName = userName;
                ViewBag.UserId = _user.Id;
                ViewBag.UserName = _user.NameSurname;
            }
         
            if(user.Role == "driver")
            {
                ViewBag.DriverId = _user.Id;
                ViewBag.DriverName = _user.NameSurname;
                ViewBag.UserId = userid;
                ViewBag.UserName = userName;
            }
           
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Messageform(CreateMessage model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.UserMessages.Add(model);
                    _dbContext.SaveChanges();

                    return RedirectToAction("Messageview", "Messages"); // Başarılıysa başka bir sayfaya yönlendirme
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Bir hata oluştu VE OLMADI: " + ex.Message);
                }
            }
            else
            {
                return NotFound();
            }
         

            return View(model);
        }
        public async Task<IActionResult> Messageview()
        {

            await SetUserInfoAsync();
            var messages = _dbContext.UserMessages.Where(m => m.UserId == _user.Id || m.DriverId==_user.Id).ToList();
            ViewBag.userRole=_user.Role;
            return View(messages);
        }

        
    }
  }
