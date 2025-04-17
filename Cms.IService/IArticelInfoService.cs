using Cms.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.IService
{
    public interface IArticelInfoService:IBaseService<ArticelInfo>
    {
        /// <summary>
        /// 保存文章图片
        /// </summary>
        /// <param name="file"></param>
        /// <param name="waterFlag"></param>
        /// <param name="contentPath"></param>
        /// <returns></returns>
        Task<string> FileUploadSave(IFormFile file,string waterFlag,string contentPath);

        /// <summary>
        /// 生成静态页
        /// </summary>
        /// <param name="articelInfo"></param>
        /// <param name="contentPath"></param>
        /// <returns></returns>
        Task CreateHtmlPage(ArticelInfo articelInfo,string contentPath);

        /// <summary>
        /// 禁用词过滤
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="memoryCache"></param>
        /// <returns></returns>
        Task<bool> FilterFobidWord(string msg,IMemoryCache memoryCache);

        /// <summary>
        /// 审查词过滤
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="memoryCache"></param>
        /// <returns></returns>
        Task<bool> FilterModWord(string msg,IMemoryCache memoryCache);

        /// <summary>
        /// 替换词
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="memoryCache"></param>
        /// <returns></returns>
        Task<string> ReplaceWord(string msg, IMemoryCache memoryCache);
    }
}
