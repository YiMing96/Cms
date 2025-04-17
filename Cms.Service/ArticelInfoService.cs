using Cms.Entity;
using Cms.IRepository;
using Cms.IService;
using Cms.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cms.Service
{
    internal class ArticelInfoService : BaseService<ArticelInfo>, IArticelInfoService
    {
        private readonly IArticelInfoRepository articelInfoRepository;
        private readonly ISensitiveWordRepository sensitiveWordRepository;
        public ArticelInfoService(IArticelInfoRepository articelInfoRepository, ISensitiveWordRepository sensitiveWordRepository)
        {
            base.repository = articelInfoRepository;
            this.articelInfoRepository = articelInfoRepository;
            this.sensitiveWordRepository = sensitiveWordRepository;
        }
        /// <summary>
        /// 生成静态页面
        /// </summary>
        /// <param name="articelInfo"></param>
        /// <param name="contentPath"></param>
        /// <returns></returns>
        public async Task CreateHtmlPage(ArticelInfo articelInfo, string contentPath)
        {
            var templateFilePath = Path.Combine(contentPath, "wwwroot", "ArticelTemplate");
            // 读取模版文件，替换占位符
            var html = Common.NVelocityHelper.RenderTemplate("ArticelTemplateInfo", articelInfo, templateFilePath);
            var dir = Path.Combine("articelHtml", articelInfo.CreatedDate.Year.ToString(), articelInfo.CreatedDate.Month.ToString(), articelInfo.CreatedDate.Day.ToString());
            var dirCombie = Path.Combine(contentPath, "wwwroot", dir);
            Directory.CreateDirectory(dirCombie);
            var fullPath = Path.Combine(dirCombie, articelInfo.Id.ToString() + ".html");
            await System.IO.File.WriteAllTextAsync(fullPath, html, System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// 实现文件的保存
        /// </summary>
        /// <param name="file"></param>
        /// <param name="waterFlag"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> FileUploadSave(IFormFile file, string waterFlag, string contentPath)
        {
            var fileNewName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var dirCombine = Path.Combine("ImageUp", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString());
            Directory.CreateDirectory(Path.Combine("wwwroot", dirCombine));
            var fullPath = Path.Combine(contentPath, "wwwroot", dirCombine, fileNewName);//完整路径
            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                if (!string.IsNullOrEmpty(waterFlag))
                {
                    //添加水印
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        using (Bitmap map = new Bitmap(memoryStream))
                        {
                            using (Graphics g = Graphics.FromImage(map))
                            {
                                g.DrawString("张三", new Font("黑体", 16.0f, FontStyle.Bold), Brushes.Red, new PointF(map.Width - 130, map.Height - 160));
                                map.Save(fileStream, ImageFormat.Jpeg);
                            }
                        }
                    }
                }
                else
                {
                    await file.CopyToAsync(fileStream);
                }
                return Path.Combine(dirCombine, fileNewName);
            }
        }

        /// <summary>
        /// 过滤禁用词
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="memoryCache"></param>
        /// <returns></returns>
        public async Task<bool> FilterFobidWord(string msg, IMemoryCache memoryCache)
        {
            var forbidList = await memoryCache.GetOrCreateAsync("ForbidWord", async (e) =>
            {
                var result = await sensitiveWordRepository.LoadEntities(s => s.IsForbid == true && s.IsDeleted == false).Select(s => s.WordPattern).ToListAsync();
                return result;
            });
            if (forbidList != null)
            {
                var reg = string.Join("|", forbidList);
                return Regex.IsMatch(msg, reg);
            }
            return false;


        }

        /// <summary>
        /// 过滤审查词
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="memoryCache"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> FilterModWord(string msg, IMemoryCache memoryCache)
        {
            var modList = await memoryCache.GetOrCreateAsync("ModWord", async (e) =>
            {
                var list = await sensitiveWordRepository.LoadEntities(s => s.IsMod == true && s.IsDeleted == false).Select(s => s.WordPattern).ToListAsync();
                return list;
            });
            if (modList != null)
            {
                var reg = string.Join("|", modList);
                return Regex.IsMatch(msg, reg);
            }
            return false;
        }

        /// <summary>
        /// 实现替换词
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="memoryCache"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> ReplaceWord(string msg, IMemoryCache memoryCache)
        {
            var replaceWordList = await memoryCache.GetOrCreateAsync("ReplaceWord", async (e) =>
            {
                var list = await sensitiveWordRepository.LoadEntities(s => s.IsForbid == false && s.IsMod == false && s.IsDeleted == false).ToListAsync();
                return list;
            });
            if (replaceWordList != null)
            {
                foreach (var item in replaceWordList)
                {
                    msg = msg.Replace(item.WordPattern!, item.ReplaceWord);
                }

            }
            return msg;
        }
    }
}
