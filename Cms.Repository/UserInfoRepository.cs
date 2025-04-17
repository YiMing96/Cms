using Cms.Entity;
using Cms.EntityFramewrokCore;
using Cms.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Repository
{
    public class UserInfoRepository : BaseRepository<UserInfo>, IUserInfoRepository
    {
       //实现的是自己独有的数据仓储的方法

        public UserInfoRepository(MyDbContext myDbContext):base(myDbContext) 
        {

        }

    }
}
