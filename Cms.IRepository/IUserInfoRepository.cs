using Cms.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cms.IRepository
{
    public interface IUserInfoRepository:IBaseRepository<UserInfo>
    {
        //定义的是属于自己的独有的数据访问的方法

    }
}
