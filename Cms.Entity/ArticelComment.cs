using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Entity
{
    /// <summary>
    /// 文章评论
    /// </summary>
    public class ArticelComment:BaseEntity<long>
    {
        public string? Msg { get; set; }
        public ArticelInfo? ArticelInfo { get; set; }
    }
}
