using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Entity
{
    /// <summary>
    /// 图片模型
    /// </summary>
    public class ImageInfo : BaseEntity<long>
    {
        public string? ImageName { get; set; }

        public int ImageServerId { get; set; }
    }
}
