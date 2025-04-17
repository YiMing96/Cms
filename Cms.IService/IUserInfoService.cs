using Cms.Entity;
using Cms.Entity.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cms.IService
{
    public interface IUserInfoService:IBaseService<UserInfo>
    {
        // 用户业务独有的方法
        IQueryable<UserInfo> LoadSearchPage(UserInfoSearch userInfoSearch, bool isDeleted);

        Task<bool> DeleteUsers(List<int> list);
    }
}
