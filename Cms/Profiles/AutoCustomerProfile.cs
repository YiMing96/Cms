using AutoMapper;
using Cms.Entity;
using Cms.Models;

namespace Cms.Profiles
{
    public class AutoCustomerProfile:Profile
    {
        public AutoCustomerProfile()
        {
            CreateMap<UserInfo, UserInfoDto>();
        }
    }
}
