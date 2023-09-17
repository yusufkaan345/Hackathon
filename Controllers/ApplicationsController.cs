using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Transportathon.Areas.Identity.Data;
using Transportathon.Data;
using Transportathon.Models;

namespace Transportathon.Controllers
{
    public class ApplicationsController : BaseController
    {
        private readonly AppDbContext _dbContext;

        public ApplicationsController(UserManager<ApplicationUser> userManager, AppDbContext dbContext) : base(userManager)
        {
            _dbContext = dbContext;
        }

        public  async Task <IActionResult> Myjobs()
        {
            await  SetUserInfoAsync();
            if (_user != null)
            {
                var userJobs = _dbContext.CreateJobs
                    .Where(j => j.UserId == _user.Id)
                    .ToList();
                return View(userJobs);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Myapplications()
        {
            await SetUserInfoAsync();
            ViewBag.UserId = _user.Id;//accepter driver id ile kendi idm aynı ise butonum accepted olacak 
            List<ApplyDriver> myapplications = _dbContext.DriverApplied.Where(u => u.DriverId == _user.Id).ToList();
            List<CreateJob> appliedJobs = myapplications
                 .Select(app => _dbContext.CreateJobs.FirstOrDefault(job => job.CreatedJobId == app.CreatedJobId))
                 .Where(job => job != null)
                    .ToList();
            return View(appliedJobs);
        }
        [HttpPost]
        public IActionResult Myapplications(int jobId)
        {
            var job = _dbContext.CreateJobs.FirstOrDefault(j => j.CreatedJobId == jobId);
            var applications = _dbContext.DriverApplied.Where(d => d.CreatedJobId == jobId).ToList();
            if (job != null)
            {
                _dbContext.CreateJobs.Remove(job);
                _dbContext.SaveChanges();

                // Başvuruları sil
                foreach (var application in applications)
                {
                    _dbContext.DriverApplied.Remove(application);
                }
                _dbContext.SaveChanges();

                return RedirectToAction("Publishedjob","Job"); // İşlem tamamlandığında başka bir sayfaya yönlendirme yapabilirsiniz
            }
            else
            {
                return NotFound(); // İş bulunamazsa 404 hatası döndürün veya başka bir hata işleme mekanizması kullanın
            }
        }



        [HttpGet]
        public async Task<IActionResult> Viewmoreandedit(int id)
        {
            await SetUserInfoAsync();

            var selectedJob = _dbContext.CreateJobs.FirstOrDefault(u => u.CreatedJobId == id);
            if (selectedJob != null)
            {
                TempData["jobId"] = selectedJob.CreatedJobId;

            }

            if (selectedJob == null)
            {
                return NotFound();
            }
            // ViewMore.cshtml sayfasına verileri iletmek için modeli kullanın.
            return View(selectedJob);
        }
        [HttpPost]
        public IActionResult Viewmoreandedit()
        {
            // Veritabanından işi alın
            if (TempData.ContainsKey("jobId"))
            {
                int jobId = (int)TempData["jobId"];
                var jobToDelete = _dbContext.CreateJobs.FirstOrDefault(j => j.CreatedJobId == jobId);

                if (jobToDelete == null)
                {

                    return NotFound(); // İş bulunamazsa 404 hatası döndürün veya başka bir hata işleme mekanizması kullanın
                }
                // İşi veritabanından silin
                _dbContext.CreateJobs.Remove(jobToDelete);
                _dbContext.SaveChanges();
                return RedirectToAction("Myapplications", "Applications"); // İşlem tamamlandığında bir başka sayfaya yönlendirme yapabilirsiniz

            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Seeapplications(int itemId)
        {
            await SetUserInfoAsync();
            TempData["jobId2"] = itemId;

            var applications = _dbContext.DriverApplied.Where(d => d.CreatedJobId == itemId ).ToList();
            var matchingUsers = new List<ApplicationUser>();
            bool isAcceptedFound = false;

            foreach (var application in applications)
            {
                var user = await _userManager.FindByIdAsync(application.DriverId);
                if (user != null)
                {
                    if (application.isAccepted)
                    {
                        var matchingUser = user; // İlk kabul edilen kullanıcıyı sakla
                        matchingUsers.Clear();
                        matchingUsers.Add(matchingUser);
                        isAcceptedFound = true;
                        break; // İlk kabul edilen kullanıcıyı bulduktan sonra döngüyü sonlandır
                    }
                }
                if (user != null)
                {
                    matchingUsers.Add(user);
                }
            }
            ViewBag.isAcceptedFound = isAcceptedFound;

            return View(matchingUsers);
        }

        [HttpPost]
        public async Task<IActionResult> Seeapplications(string driverId)
        {
            if (TempData.ContainsKey("jobId2"))
            {
                int jobId = (int)TempData["jobId2"];
                var jobToUpdate = _dbContext.CreateJobs.FirstOrDefault(j => j.CreatedJobId == jobId);
                if (jobToUpdate == null)
                {
                    return NotFound(); // İş bulunamazsa 404 hatası döndürün veya başka bir hata işleme mekanizması kullanın
                }
                jobToUpdate.Accepted = true;
                jobToUpdate.DriverId = driverId;

                var appliedDriver = _dbContext.DriverApplied.FirstOrDefault(ad => ad.DriverId == driverId && ad.CreatedJobId == jobId);
                if (appliedDriver != null)
                {
                    appliedDriver.isAccepted = true;
                }
                _dbContext.SaveChanges();



                return RedirectToAction("Myjobs", "Applications");

               
            }
            else
            {
                return NotFound();
            }           
        }

        [HttpGet]
        public async Task<IActionResult> Review(string driverid)
        {
            await SetUserInfoAsync();

            //Task init işlemleri
            var model = new Review();
          
            model.TaskId = 1;
            ViewBag.UserId = _user.UserName;
            ViewBag.DriverId = driverid;
            return View(model);
        }

        [HttpPost]
        public IActionResult Review(Review model)
        {
            
                    _dbContext.DriverReviews.Add(model);
                    _dbContext.SaveChanges();

                    return RedirectToAction("Myjobs", "Applications"); // Başarılıysa başka bir sayfaya yönlendirme
                 
        }

        [HttpGet]
        public async Task<IActionResult> Seereviews(string driverid)
        {
            await SetUserInfoAsync();

            var reviews = _dbContext.DriverReviews.Where(r => r.DriverId == driverid).ToList();

            return View(reviews);
        }


        }
    }
