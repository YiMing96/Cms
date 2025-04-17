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
    public class ImageInfoService : BaseService<ImageInfo>, IImageInfoService
    {
        private readonly IImageInfoRepository imageInfoRepository;
        public ImageInfoService(IImageInfoRepository imageInfoRepository)
        {
            this.imageInfoRepository = imageInfoRepository;
            base.repository = imageInfoRepository;
        }
    }
}
