using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Entity.Enum
{
    public enum DelFlag
    {
        /// <summary>
        /// 逻辑删除
        /// </summary>
        LogicDelete = 1,
        /// <summary>
        /// 正常数据
        /// </summary>
        Normal = 0
    }
}
