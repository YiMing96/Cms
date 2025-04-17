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
    public class ImageServerInfoService : BaseService<ImageServerInfo>, IImageServerInfoService
    {
        private readonly IImageServerInfoRepository imageServerInfoRepository;
        public ImageServerInfoService(IImageServerInfoRepository imageServerInfoRepository)
        {
            this.imageServerInfoRepository = imageServerInfoRepository;
            base.repository = imageServerInfoRepository;

        }
    }
}
