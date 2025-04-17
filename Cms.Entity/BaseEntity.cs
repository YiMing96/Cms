using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Entity
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }

        /// <summary>
        /// 添加记录时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 更新记录的时间
        /// </summary>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// 软删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
