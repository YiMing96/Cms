using Cms.Entity;
using Cms.Entity.Enum;
using Cms.Entity.Search;
using Cms.IRepository;
using Cms.IService;
using Cms.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Service
{
    public class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        private readonly IUserInfoRepository _userInfoRepository;
        public UserInfoService(IUserInfoRepository userInfoRepository)
        {
            base.repository = userInfoRepository;
            this._userInfoRepository = userInfoRepository;

        }
        /// <summary>
        /// 批量删除用户信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> DeleteUsers(List<int> list)
        {
            var users = _userInfoRepository.LoadEntities(u => list.Contains(u.Id));
            foreach (var user in users)
            {
                user.IsDeleted = Convert.ToBoolean(DelFlag.LogicDelete);
                await _userInfoRepository.UpdateEntityAsync(user);
            }

            return await Task.FromResult(true);


        }

        /// <summary>
        /// 多条件搜索
        /// </summary>
        /// <param name="userInfoSearch"></param>
        /// <param name="isDeleted"></param>
        /// <returns></returns>
        public IQueryable<UserInfo> LoadSearchPage(UserInfoSearch userInfoSearch, bool isDeleted)
        {
            var temp = _userInfoRepository.LoadEntities(u => u.IsDeleted == isDeleted);
            if (!string.IsNullOrEmpty(userInfoSearch.UserName))
            {
                temp = temp.Where(u => u.UserName!.Contains(userInfoSearch.UserName));
            }
            if (!string.IsNullOrEmpty(userInfoSearch.UserEmail))
            {
                temp = temp.Where(u => u.UserEmail!.Contains(userInfoSearch.UserEmail));
            }
            userInfoSearch.TotalCount = temp.Count();
            if (userInfoSearch.OrderBy)
            {
                temp = temp.OrderBy(u => u.Id).Skip((userInfoSearch.PageIndex - 1) * userInfoSearch.PageSize).Take(userInfoSearch.PageSize);
            }
            else
            {
                temp = temp.OrderByDescending(u => u.Id).Skip((userInfoSearch.PageIndex - 1) * userInfoSearch.PageSize).Take(userInfoSearch.PageSize);
            }

            return temp;



        }
    }


}
