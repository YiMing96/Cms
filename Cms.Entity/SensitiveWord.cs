using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Entity
{
    public class SensitiveWord : BaseEntity<long>
    {
        // 词名称
        public string? WordPattern { get; set; }
        // 是否是禁用词
        public bool IsForbid { get; set; }
        // 是否是审查词
        public bool IsMod { get; set; }
        // 替换词
        public string? ReplaceWord { get; set; }
    }
}
