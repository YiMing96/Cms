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
    public class ArticelClassService:BaseService<ArticelClass>,IArticelClassService
    {
        private readonly IArticelClassRepository articelClassRepository;
        public ArticelClassService(IArticelClassRepository articelClassRepository) 
        {
            base.repository = articelClassRepository;
            this.articelClassRepository = articelClassRepository;
        }
    }
}
