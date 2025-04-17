using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Entity
{
    public class VideoInfo : BaseEntity<long>
    {
        /// <summary>
        /// 视频的名称，非全路径
        /// </summary>
        public string? VideoName { get; set; }
        /// <summary>
        /// 文件大小（尺寸为字节）
        /// </summary>
        public long? FileSizeInBytes { get; set; }

        /// <summary>
        /// 两个文件的大小和散列值（SHA256）都相同的概率非常小。因此只要大小和SHA256相同，就认为是相同的文件。
        /// SHA256的碰撞的概率比MD5低很多。
        /// </summary>
        public string? FileSHA256Hash { get; set; }

        /// <summary>
        /// 视频转码后的存储路径
        /// </summary>
        public string? VideoPath { get; set; }

        /// <summary>
        /// 待转码的视频文件路径
        /// </summary>
        public string? SourcePath { get; set; }
        /// <summary>
        /// 视频简介
        /// </summary>
        public string? VideoContent { get; set; }

        /// <summary>
        /// 视频作者
        /// </summary>
        public string? Author { get; set; }

        /// <summary>
        /// 视频截图存储路径
        /// </summary>
        public string? ImageUrl { set; get; }
        /// <summary>
        /// 视频状态，初始值为0，当视频上传到视频文件服务器状态为1，转码成功后修改为2
        /// </summary>
        public int Status { get; set; }

    }
}
