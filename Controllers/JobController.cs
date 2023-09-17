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
            model.DriverId = "1";
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

                    return RedirectToAction("Publishedjob", "Job"); // Başarılıysa başka bir sayfaya yönlendirme
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Bir hata oluştu VE OLMADI: " + ex.Message);
                }
            }
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        ModelState.AddModelError(string.Empty, "Bir hata oluştu VE OLMADI: " + error.ErrorMessage);
                    }
                }
            }

            return View(model);
        }


        public async Task<IActionResult> Publishedjob()
        {
            await SetUserInfoAsync();
            List<CreateJob> jobs = _dbContext.CreateJobs.ToList();
            List<ApplyDriver> applications = _dbContext.DriverApplied.ToList();

            string userId = _user.Id;

            // Aynı CreatedJobId'ye sahip işleri ve başvuruları bulun
            var matchedJobIds = applications
                .Where(app => app.DriverId == userId)
                .Select(app => app.CreatedJobId)
                .ToList();

            // Belirli bir koşulu sağlayan işleri jobs listesinden çıkarın
            jobs.RemoveAll(job => matchedJobIds.Contains(job.CreatedJobId));
            return View(jobs);
        }

        [HttpGet]
        public async Task<IActionResult> Viewmore(int id)
        {
            await SetUserInfoAsync();


            // Veritabanından başvuru yapılmak istenen CreatedJobId'sine göre viewmoreye bilgi gönderiyor.

            var selectedJob = _dbContext.CreateJobs.FirstOrDefault(u => u.CreatedJobId == id);

            var userRole = "";
            if (selectedJob == null)
            {
                return NotFound();
            }

            //Viewmore sayfasına userın role bilgisini gönderiyor
            if (_user != null)
            {
                userRole = _user.Role;
                TempData["jobId"] = selectedJob.CreatedJobId;  //Bu satırın amacı get ile adlığımız createdJobİDsini post metodunda kullanmak.Çünkü postta girdi parametresi alamıyorsun 
            }
            ViewBag.UserRole = userRole;

            // ViewMore.cshtml sayfasına verileri iletmek için modeli kullanın.
            return View(selectedJob);
        }

        [HttpPost]
        public IActionResult Viewmore()
        {
            if (ModelState.IsValid)
            {
                if (TempData.ContainsKey("jobId"))
                {
                    int jobId = (int)TempData["jobId"];
                    var userPrincipal = HttpContext.User;
                    var userId = _userManager.GetUserId(userPrincipal);
                    var applyDriver = new ApplyDriver
                    {
                        // ApplyDriver modelinin alanlarını ViewModel'den gelen verilere ayarlayın
                        DriverId = userId,
                        CreatedJobId = jobId,
                        isAccepted = false // Varsayılan olarak false olarak ayarladım, gerektiğine göre değiştirin
                    };
                    _dbContext.DriverApplied.Add(applyDriver);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Myapplications", "Applications");


                }
                else
                {
                    // TempData'da jobId bulunmuyor, bir hata işleyebilirsiniz
                    TempData["ErrorMessage"] = "jobId verisi eksik.";
                    // veya ModelState'a hata ekleyebilirsiniz
                    ModelState.AddModelError("jobId", "jobId verisi eksik.");

                    // Hata işleme yöntemine göre farklı bir işlem yapabilirsiniz, örneğin geri dönüş yapabilirsiniz
                    return View(); // Hata sayfasına yönlendirme
                }
            }
                return View();

        }
    }
}
