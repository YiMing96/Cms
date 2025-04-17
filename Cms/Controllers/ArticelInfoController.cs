using Cms.Attributes;
using Cms.Entity;
using Cms.Entity.Enum;
using Cms.EntityFramewrokCore;
using Cms.IService;
using Cms.Models;
using Cms.Service;
using Cms.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Cms.Controllers
{
    public class ArticelInfoController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IArticelInfoService articelInfoService;
        private readonly IArticelClassService articelClassService;
        private readonly IArticelCommentService articelCommentService;
        private readonly MyDbContext myDbContext;
        private readonly IMemoryCache memoryCache;
        public ArticelInfoController(IWebHostEnvironment webHostEnvironment, IArticelInfoService articelInfoService, IArticelClassService articelClassService, MyDbContext myDbContext, IMemoryCache memoryCache, IArticelCommentService articelCommentService)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.articelInfoService = articelInfoService;
            this.articelClassService = articelClassService;
            this.myDbContext = myDbContext;
            this.memoryCache = memoryCache;
            this.articelCommentService = articelCommentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 呈现添加新闻的视图
        /// </summary>
        /// <returns></returns>
        public IActionResult ShowAdd()
        {
            return View();
        }

        /// <summary>
        /// 获取文章列表数据
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetArticels()
        {
            bool page = int.TryParse(Request.Form["pageIndex"], out int pageIndex);
            bool size = int.TryParse(Request.Form["pageSize"], out int pageSize);
            if (!page || !size)
            {
                return Json(new { code = 500, message = "参数错误" });

            }

            int totalCount = 0;
            var articels = await articelInfoService.LoadPageEntities(pageIndex + 1, pageSize, out totalCount, a => a.IsDeleted == Convert.ToBoolean(DelFlag.Normal), a => a.Id, true).Select(a => new { Id = a.Id, Title = a.Title, Author = a.Author, Origin = a.Origin, CreatedDate = a.CreatedDate, ClassName = a.ArticelClass!.ArticelClassName }).ToListAsync();
            return Json(new { total = totalCount, data = articels });
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> FileUpload()
        {
            var file = Request.Form.Files["avator"];
            if (file == null)
            {
                return Json(new { code = 404, message = "请选择文件" });
            }
            var fileExt = Path.GetExtension(file.FileName);
            if (fileExt != ".jpg")
            {
                return Json(new { code = 501, message = "文件类型错误" });
            }
            var filePath = await articelInfoService.FileUploadSave(file, Request.Query["waterFlag"]!, webHostEnvironment.ContentRootPath);
            return Json(new { code = 200, path = filePath });
        }

        /// <summary>
        /// 获取文章类别的数据
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetCategory()
        {
            var categories = await articelClassService.LoadEntities(c => c.IsDeleted == Convert.ToBoolean(DelFlag.Normal)).ToListAsync();

            return Json(new { code = 200, data = categories });
        }

        //[UnitofWork(new Type[] { typeof(MyDbContext) })]
        public async Task<IActionResult> AddArticel([FromForm] ArticelInfo articelInfo)
        {
            var articelClassId = Convert.ToInt32(Request.Form["ArticelClassInfo"]);
            //var articelClass = await articelClassService.LoadEntities(c=>c.Id==articelClassId).FirstOrDefaultAsync();
            articelInfo.UpdatedDate = DateTime.Now;
            articelInfo.CreatedDate = DateTime.Now;
            articelInfo.IsDeleted = Convert.ToBoolean(DelFlag.Normal);
            //articelInfo.ArticelClass = articelClass;
            articelInfo.ArticelClassId = articelClassId;

            myDbContext.ArticelInfos.Add(articelInfo);
            await myDbContext.SaveChangesAsync();
            //对添加文章生成静态页面
            await articelInfoService.CreateHtmlPage(articelInfo, webHostEnvironment.ContentRootPath);

            return Json(new { code = 200, message = "文章添加成功" });

        }

        /// <summary>
        /// 添加评论
        /// </summary>
        /// <returns></returns>
        [UserInfo(new Type[] { typeof(MyDbContext) })]
        public async Task<IActionResult> AddComment([FromBody] ArticelCommentDto articelCommentDto)
        {
            //articelCommentDto.Msg.Replace("<","&lt;");

            if (await articelInfoService.FilterFobidWord(articelCommentDto.Msg!, memoryCache))
            {
                return Json(new { code = 501, message = "评论中含有禁用词" });
            }
            else if (await articelInfoService.FilterModWord(articelCommentDto.Msg!, memoryCache))
            {
                ArticelComment articelComment = new ArticelComment();
                articelComment.UpdatedDate = DateTime.Now;
                articelComment.CreatedDate = DateTime.Now;
                articelComment.Msg = articelCommentDto.Msg;
                articelComment.IsDeleted = Convert.ToBoolean(DelFlag.LogicDelete);
                articelComment.ArticelInfo = await articelInfoService.LoadEntities(a => a.Id == articelCommentDto.ArticelId).FirstOrDefaultAsync();
                await articelCommentService.InsertEntityAsync(articelComment);
                return Json(new { code = 201, message = "评论中含有审查词，需要管理员审核!" });

            }
            else
            {
                ArticelComment articelComment = new ArticelComment();
                articelComment.UpdatedDate = DateTime.Now;
                articelComment.CreatedDate = DateTime.Now;
                articelComment.Msg = await articelInfoService.ReplaceWord(articelCommentDto.Msg!, memoryCache);// 替换词实现
                articelComment.IsDeleted = Convert.ToBoolean(DelFlag.Normal);// 表示评论可以展示
                articelComment.ArticelInfo = await articelInfoService.LoadEntities(a => a.Id == articelCommentDto.ArticelId).FirstOrDefaultAsync();
                await articelCommentService.InsertEntityAsync(articelComment);
                return Json(new { code = 200, message = "评论发布成功了!" });
            }
        }

        /// <summary>
        /// 给所有文章生成静态页面
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CreateAllPage()
        {
            var articels = await articelInfoService.LoadEntities(a => a.IsDeleted == Convert.ToBoolean(DelFlag.Normal)).ToListAsync();
            foreach (var articel in articels)
            {
                await articelInfoService.CreateHtmlPage(articel, webHostEnvironment.ContentRootPath);
            }

            return Json(new { code = 200, message = "生成静态页面成功" });
        }

        /// <summary>
        /// 加载评论
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> LoadComment()
        {
            bool b = int.TryParse(Request.Query["articelId"], out int articelId);
            if (!b)
            {
                return Json(new { code = 500, message = "参数错误" });
            }
            var results = await articelCommentService.LoadEntities(a => a.ArticelInfo!.Id == articelId && a.IsDeleted == Convert.ToBoolean(DelFlag.Normal)).ToListAsync();
            List<ArticelCommentDto> list = new List<ArticelCommentDto>();
            foreach (var articel in results)
            {
                ArticelCommentDto articelCommentDto = new ArticelCommentDto();

                articelCommentDto.Msg = articel.Msg;
                TimeSpan ts = DateTime.Now - articel.CreatedDate;
                articelCommentDto.CreatedDate = CommonHelper.GetTimeSpan(ts);

                list.Add(articelCommentDto);
            }
            /*   TimeSpan ts=   DateTime.Now-评论发布的时间
                      ts--"1分钟前"*/


            return Json(new { code = 200, data = list });
        }

        /// <summary>
        /// 更新文章访问次数
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> EditCount()
        {
            var articelId = Convert.ToInt32(Request.Query["articelId"]);
            var  articelInfo= await articelInfoService.LoadEntities(a => a.Id == articelId).FirstOrDefaultAsync();
            if (articelInfo == null)
            {
                return Json(new { code = 404, msg = "文章不存在" });
            }
            QueueHelper.QueueEnque(articelId);
            return Json(new { code = 200,changes=articelInfo.Changes });
        }
    }
}
