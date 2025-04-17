using Cms.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.IService
{
    public interface IVideoInfoService : IBaseService<VideoInfo>
    {
        Task<VideoInfo> UploadVideoInfo(string author, string content, Stream stream, string fileName);
    }
}
