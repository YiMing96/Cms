using Cms.Entity;
using Cms.EntityFramewrokCore;
using Cms.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Repository
{
    public class ImageServerInfoRepository : BaseRepository<ImageServerInfo>, IImageServerInfoRepository
    {
        public ImageServerInfoRepository(MyDbContext myDbContext) : base(myDbContext) { }
    }
}
