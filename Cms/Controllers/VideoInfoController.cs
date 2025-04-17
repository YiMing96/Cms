using Cms.Attributes;
using Cms.EntityFramewrokCore;
using Cms.IService;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Controllers
{
    public class VideoInfoController : Controller
    {
        private readonly IVideoInfoService videoInfoService;
        public VideoInfoController(IVideoInfoService videoInfoService)
        {
            this.videoInfoService = videoInfoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [RequestSizeLimit(60_000_000)]
        [UserInfo(new Type[] { typeof(MyDbContext)})]
        public async Task<IActionResult> FileUpload(IFormFile filePath)
        {
            string author = Request.Form["author"]!;
            string videoContent = Request.Form["videoContent"]!;
            using (var stream = filePath.OpenReadStream())
            {
                var videoInfo = await videoInfoService.UploadVideoInfo(author, videoContent, stream, filePath.FileName);
                return Json(new { code = 200, data = videoInfo });
            }
        }
    }
}
