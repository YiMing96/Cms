using Cms.Video.FileService;
using Cms.Video.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cms.Video.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FSDomainService fSDomainService;
        private readonly IWebHostEnvironment webHostEnvironment;
        public HomeController(ILogger<HomeController> logger, FSDomainService fSDomainService, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            this.fSDomainService = fSDomainService;
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
        [RequestSizeLimit(60_000_000)]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var url = await fSDomainService.SaveVidoInfo(stream, file.FileName, webHostEnvironment.ContentRootPath);

                return Content(url);
            }
        }
    }
}
