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
    public class ArticelCommentService : BaseService<ArticelComment>, IArticelCommentService
    {
        private readonly IArticelCommentRepository articelCommentRepository;
        public ArticelCommentService(IArticelCommentRepository articelCommentRepository)
        {
            base.repository = articelCommentRepository;
            this.articelCommentRepository = articelCommentRepository;
        }
    }
}
