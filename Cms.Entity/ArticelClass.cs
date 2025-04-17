using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Entity
{
    public class ArticelClass : BaseEntity<int>
    {
        public string? ArticelClassName { get; set; }

        public int ParentId { get; set; }
        public string? Remark { get; set; }
        /// <summary>
        /// /类别下面很多文章
        /// </summary>
        public List<ArticelInfo> ArticelInfos { get; set; } = new List<ArticelInfo>();
    }
}
