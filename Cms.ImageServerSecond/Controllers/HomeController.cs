using Cms.ImageServerSecond.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cms.ImageServerSecond.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Upload(IFormFile file)
        {
            string fileName = file.FileName;
            using (var stream = file.OpenReadStream())
            {
                DateTime today = DateTime.Today;
                string hashPath = Common.HashHelper.ComputeSha256Hash(stream);
                string dir = Path.Combine("images", today.Year.ToString(), today.Month.ToString(), today.Day.ToString(),hashPath);
                stream.Position = 0;//流一定要返回到开始
                var dirCombine = Path.Combine(webHostEnvironment.ContentRootPath, "wwwroot", dir);

                Directory.CreateDirectory(dirCombine);
                var fullPath = Path.Combine(dirCombine, fileName);
                using (var outStream = System.IO.File.OpenWrite(fullPath))
                {
                    await stream.CopyToAsync(outStream);
                }
                var request = HttpContext.Request;
                var url = request.Scheme + "://" + request.Host + "/" + dir+"/"+fileName;
                return Content(url);
            }
        }
    }
}
