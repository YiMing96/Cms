using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Video.FileService
{
    public class FSDomainService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public FSDomainService(IHttpContextAccessor _httpContextAccessor)
        {
            this._httpContextAccessor = _httpContextAccessor;

        }
        public async Task<string> SaveVidoInfo(Stream stream, string fileName, string contentPath)
        {


            DateTime today = DateTime.Today;
            string hashPath = Common.HashHelper.ComputeSha256Hash(stream);
            string dir = Path.Combine("images", today.Year.ToString(), today.Month.ToString(), today.Day.ToString(), hashPath);

            stream.Position = 0;
            var dirCombie = Path.Combine(contentPath, "wwwroot", dir);
            Directory.CreateDirectory(dirCombie);


            var fullPath = Path.Combine(dirCombie, fileName);
            using (var outStream = System.IO.File.OpenWrite(fullPath))
            {
                await stream.CopyToAsync(outStream);
            }

            var request = _httpContextAccessor.HttpContext.Request;
            var url = request.Scheme + "://" + request.Host + "/" + dir + "/" + fileName;// http://localhost/images/2024/
            return url;

        }
    }
}
