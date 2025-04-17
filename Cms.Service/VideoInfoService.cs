using Cms.Entity;
using Cms.IRepository;
using Cms.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Service
{
    public class VideoInfoService : BaseService<VideoInfo>, IVideoInfoService
    {
        private readonly IVideoInfoRepository videoInfoRepository;
        private readonly IHttpClientFactory httpClientFactory;
        public VideoInfoService(IVideoInfoRepository videoInfoRepository, IHttpClientFactory httpClientFactory)
        {
            this.videoInfoRepository = videoInfoRepository;
            this.httpClientFactory = httpClientFactory;
        }
        /// <summary>
        /// 将视频文件上传到视频文件服务器，上传成功以后，将视频的信息存储到视频文件信息表中。
        /// </summary>
        /// <param name="author"></param>
        /// <param name="content"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public async Task<VideoInfo> UploadVideoInfo(string author, string content, Stream stream, string fileName)
        {
            // 判断视频文件是否已经存在，如果存在，则不上传。
            var stremHash = Common.HashHelper.ComputeSha256Hash(stream);
            var videoSize = stream.Length;
            var video = await videoInfoRepository.LoadEntities(a => a.FileSHA256Hash == stremHash && a.FileSizeInBytes == videoSize).FirstOrDefaultAsync();

            stream.Position = 0;// !!!!!!!
            if (video != null)
            {
                return video;
            }
            using (MultipartFormDataContent formDataContent = new MultipartFormDataContent())
            {

                using (var streamContent = new StreamContent(stream))
                {
                    formDataContent.Add(streamContent, "file", fileName);

                    var httpClient = httpClientFactory.CreateClient();
                    Uri uri = new Uri("http://localhost:5024" + "/Home/Upload");

                    var responseResult = await httpClient.PostAsync(uri, formDataContent); // 开始向图片服务器发送文件数据，并且返回响应的结果。
                    if (responseResult.IsSuccessStatusCode)
                    {
                        VideoInfo videoInfo = new VideoInfo();
                        videoInfo.Author = author;
                        videoInfo.VideoContent = content;
                        videoInfo.CreatedDate = DateTime.Now;
                        videoInfo.UpdatedDate = DateTime.Now;
                        videoInfo.Status = 1;// 表示视频文件已经上传到视频文件服务器
                        videoInfo.VideoPath = "";
                        videoInfo.FileSizeInBytes = videoSize;
                        videoInfo.FileSHA256Hash = stremHash;
                        videoInfo.ImageUrl = "";
                        videoInfo.IsDeleted = false;
                        videoInfo.SourcePath = await responseResult.Content.ReadAsStringAsync();
                        await videoInfoRepository.InsertEntityAsync(videoInfo);

                        return videoInfo;
                    }
                    else
                    {
                        return null;
                    }
                }

            }
        }
    }
}
