using AutoMapper;
using Cms.Attributes;
using Cms.Entity;
using Cms.Entity.Enum;
using Cms.Entity.Search;
using Cms.EntityFramewrokCore;
using Cms.IService;
using Cms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cms.Controllers
{
    public class UserInfoController : Controller
    {
        private readonly IUserInfoService userInfoService;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        public UserInfoController(IUserInfoService userInfoService, IMapper mapper,IWebHostEnvironment webHostEnvironment)
        {
            this.userInfoService = userInfoService;
            this.mapper = mapper;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            /*var list = userInfoService.LoadEntities(u => u.IsDeleted == false).ToList();
            var listDto= mapper.Map<List < UserInfoDto >> (list);*/
            //return Json(listDto);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetUsers()
        {
            bool page = int.TryParse(Request.Form["pageIndex"], out int pageIndex);
            bool size = int.TryParse(Request.Form["pageSize"], out int pageSize);
            if (!page || !size)
            {
                return Json(new { code = 500, message = "参数错误" });

            }
            string uname = Request.Form["uname"]!;
            string uemail = Request.Form["uemail"]!;
            string sortOrder = string.IsNullOrEmpty(Request.Form["sortOrder"]) ? "asc" : Request.Form["sortOrder"].ToString();
            bool isAsc = sortOrder == "asc" ? true : false;
            int totalCount = 0;
            UserInfoSearch userInfoSearch = new UserInfoSearch()
            {
                OrderBy = isAsc,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                TotalCount = totalCount,
                UserEmail = uemail,
                UserName = uname,
            };
            var isDelete = Convert.ToBoolean(DelFlag.Normal);
            //var users = await userInfoService.LoadPageEntities(pageIndex + 1, pageSize, out totalCount, u => u.IsDeleted == false, u => u.Id, isAsc).ToListAsync();
            var users = await userInfoService.LoadSearchPage(userInfoSearch, isDelete).ToListAsync();
            var usersList = mapper.Map<List<UserInfoDto>>(users);
            return Json(new { total = userInfoSearch.TotalCount, data = usersList });
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <returns></returns>
        [UserInfo(new Type[] { typeof(MyDbContext) })]
        public async Task<IActionResult> DeleteUser()
        {
            bool b = int.TryParse(Request.Query["uid"], out int userId);
            if (!b)
            {
                return Json(new { code = 500, message = "参数错误" });
            }
            var user = await userInfoService.LoadEntities(u=>u.Id==userId).FirstOrDefaultAsync();
            if (user == null)
            {
                return Json(new { code = 501, message = "用户不存在" });
            }
            user.IsDeleted = Convert.ToBoolean(DelFlag.LogicDelete);
            await userInfoService.UpdateEntityAsync(user);
            return Json(new { code = 200, message = "删除成功" });
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <returns></returns>
        [UserInfo(new Type[] { typeof(MyDbContext) })]
        public async Task<IActionResult> DeleteUsers()
        {
            string userId = Request.Query["userId"]!;
            var strIds = userId.Split(',');
            List<int>list = new List<int>();
            foreach (var id in strIds)
            {
                list.Add(Convert.ToInt32(id));
            }
            await userInfoService.DeleteUsers(list);
            return Json(new { code = 200, message = "删除成功" });
        }

        public IActionResult ShowAdd()
        {
            return View();
        }

        /// <summary>
        /// 上传头像
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> FileUpLoad()
        {
            var file = Request.Form.Files["avator"];
            if (file == null)
            {
                return Json(new { code = 500, message = "请选择文件" });
            }
            var newfileName = Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
            var filePath = Path.Combine(webHostEnvironment.ContentRootPath, "wwwroot", "images");
            var fullPath = Path.Combine(filePath, newfileName);
            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                await file.CopyToAsync(fileStream);
            }
            return Json(new { code = 200, dir = "/images/" + newfileName });
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [UserInfo(new Type[] { typeof(MyDbContext) })]
        public async Task<IActionResult> CreateUser([FromBody]UserInfo userInfo)
        {
            /*UserInfo userInfo = new UserInfo()
            {
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Gender = 0,
                IsDeleted = false,
                PhotoUrl = "b.jpg",
                UserEmail = "zhangsan@126.com",
                UserName = "zhangsan",
                UserPassword = "123465",
                UserPhone = "12345678901",
            };
            await userInfoService.InsertEntityAsync(userInfo);
            return Content("OK");*/
            userInfo.IsDeleted = Convert.ToBoolean(DelFlag.Normal);
            userInfo.CreatedDate = DateTime.Now;
            userInfo.UpdatedDate = DateTime.Now;
            await userInfoService.InsertEntityAsync(userInfo);
            return Json(new { code =200,message="添加用户成功"});
        }

        public async Task<IActionResult> ShowEdit()
        {
            bool b = int.TryParse(Request.Query["userId"], out int userId);
            if (!b) 
            {
                return Json(new { code = 501,message = "用户编号错误"});
            }
            var user = await userInfoService.LoadEntities(u => u.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                return Json(new { code =502,message="用户不存在"});
            }

            return View(user);
        }

        [UserInfo(new Type[] { typeof(MyDbContext) })]
        public async Task<IActionResult> EditUser([FromForm]UserInfo userInfo)
        {
            //var user= await userInfoService.LoadEntities(u => u.Id == userInfo.Id).FirstOrDefaultAsync();
            //user
            userInfo.UpdatedDate = DateTime.Now;
            userInfo.IsDeleted = Convert.ToBoolean(DelFlag.Normal);
            await userInfoService.UpdateEntityAsync(userInfo);

            return Json(new { code = 200, message = "更新成功" });
        }
    }
}
