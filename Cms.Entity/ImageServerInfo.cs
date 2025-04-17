using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Entity
{
    /// <summary>
    /// 图片服务器模型
    /// </summary>
    public class ImageServerInfo : BaseEntity<long>
    {
        /// <summary>
        /// 服务器名称
        /// </summary>
        public string? ServerName { get; set; }

        /// <summary>
        /// 服务器地址
        /// </summary>
        public string? ServerUrl { get; set; }

        /// <summary>
        /// 图片服务器最多能存储多少张图片
        /// </summary>
        public long MaxPicAccount { get; set; }
        /// <summary>
        /// 当前存储了多少张图片
        /// </summary>
        public long CurrPicAccount { get; set; }
    }
}
