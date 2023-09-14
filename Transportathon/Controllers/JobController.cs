using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using Transportathon.Areas.Identity.Data;
using Transportathon.Data;
using Transportathon.Models;
using Microsoft.EntityFrameworkCore;


namespace Transportathon.Controllers
{
    public class JobController : BaseController
    {
        
        private readonly AppDbContext _dbContext;
        public JobController(UserManager<ApplicationUser> userManager, AppDbContext dbContext) : base(userManager)
        {
            _dbContext = dbContext;
        }
       

        public async Task<IActionResult> Createjob()
        {
           await SetUserInfoAsync();

            //Task init işlemleri
            var model = new CreateJob();
            model.PublishDate = DateTime.Now;
            if (_user != null)
            {
                model.UserId = _user.Id;
                model.UserName = _user.NameSurname;
            }
            model.DriverId = 1;
            model.Status = true;
            model.Accepted = false;

            return View(model);
        }

        [HttpPost]
        public IActionResult Createjob(CreateJob model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.CreateJobs.Add(model);
                    _dbContext.SaveChanges();

                    return RedirectToAction("Index", "Home"); // Başarılıysa başka bir sayfaya yönlendirme
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Bir hata oluştu VE OLMADI: " + ex.Message);
                }
            }

            return View(model);
        }


        public async Task<IActionResult> Publishedjob()
        {
            await SetUserInfoAsync();
            List<CreateJob> jobs = _dbContext.CreateJobs.ToList();
            return View(jobs);
        }

    }
}
