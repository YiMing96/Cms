using Cms.Attributes;
using Cms.Entity;
using Cms.Entity.Enum;
using Cms.EntityFramewrokCore;
using Cms.IService;
using Cms.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cms.Controllers
{
    public class ImageInfoController : Controller
    {
        private readonly IImageServerInfoService imageServerInfoService;
        private readonly IImageInfoService imageInfoService;
        private readonly IHttpClientFactory httpClientFactory;
        public ImageInfoController(IImageServerInfoService imageServerInfoService, IHttpClientFactory httpClientFactory, IImageInfoService imageInfoService)
        {
            this.imageServerInfoService = imageServerInfoService;
            this.httpClientFactory = httpClientFactory;
            this.imageInfoService = imageInfoService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [UserInfo(new Type[] { typeof(MyDbContext) })]
        public async Task<IActionResult> FileUpload(IFormFile formFile)
        {
            if (formFile == null)
            {
                return Json(new { code = 404, message = "请选择要上传的文件" });
            }
            string fileName = formFile.FileName;
            var imageServices = await imageServerInfoService.LoadEntities(a => a.IsDeleted == Convert.ToBoolean(DelFlag.Normal)).ToListAsync();
            var count = imageServices.Count();
            var random = new Random();
            var i = random.Next(0, count);
            var serverInfo = imageServices[i]; //存储的就是获取到的图片文件服务的信息
            using (MultipartFormDataContent formDataContent = new MultipartFormDataContent())
            {
                using (var stream = formFile.OpenReadStream())
                {
                    using (var streamContent = new StreamContent(stream))
                    {
                        formDataContent.Add(streamContent, "file", fileName);

                        //formDataContent = _httpClientFactory.CreateClient();
                        var httpClient = httpClientFactory.CreateClient();
                        Uri uri = new Uri(serverInfo.ServerUrl + "/Home/Upload");
                        var responseResult = await httpClient.PostAsync(uri, formDataContent);//开始向图片服务器发送文件数据，并且返回响应的结果
                        if (responseResult.IsSuccessStatusCode)
                        {
                            //存储图片信息
                            ImageInfo imageInfo = new ImageInfo();
                            imageInfo.ImageServerId =Convert.ToInt32( serverInfo.Id);
                            imageInfo.CreatedDate = DateTime.Now;
                            imageInfo.UpdatedDate = DateTime.Now;
                            imageInfo.IsDeleted = Convert.ToBoolean(DelFlag.Normal);
                            imageInfo.ImageName =await responseResult.Content.ReadAsStringAsync();//获取到了图片服务器返回的图片路径
                            await imageInfoService.InsertEntityAsync(imageInfo);
                        }
                    }
                }
            }
            return Content("上传成功");
        }

        /// <summary>
        /// 展示图片
        /// </summary>
        /// <returns></returns>
        public IActionResult ShowImages()
        {
            return View();
        }

        /// <summary>
        /// 获取图片数据
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetImages()
        {
            var list = await imageInfoService.LoadEntities(a => a.IsDeleted == Convert.ToBoolean(DelFlag.Normal)).ToListAsync();
            return Json(new { code = 200, data = list });
        }
    }
}
