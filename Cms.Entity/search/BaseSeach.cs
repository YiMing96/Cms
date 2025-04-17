using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Entity.Search
{
    public class BaseSeach
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public bool OrderBy { get; set; }
    }
}
