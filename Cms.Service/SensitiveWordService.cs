using Cms.Entity;
using Cms.IRepository;
using Cms.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Service
{
    public class SensitiveWordService : BaseService<SensitiveWord>, ISensitiveWordService
    {
        private readonly ISensitiveWordRepository sensitiveWordRepository;
        public SensitiveWordService(ISensitiveWordRepository sensitiveWordRepository)
        {
            this.sensitiveWordRepository = sensitiveWordRepository;

        }
    }
}
