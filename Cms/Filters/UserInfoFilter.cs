using Cms.Attributes;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Cms.Filters
{
    public class UserInfoFilter : IAsyncActionFilter
    {

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var result = await next();
            if (result.Exception != null)
            {
                //所请求的控制器下的方法中执行有错误
                return;
            }
            var controllerActionDes = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDes == null)
            {
                return;
            }
            var user = controllerActionDes.MethodInfo.GetCustomAttribute<UserInfoAttribute>();
            if (user == null)
            {
                return;
            }
            foreach (var dbType in user.DbContextType)
            {
                var dbContext= context.HttpContext.RequestServices.GetService(dbType) as DbContext;
                if (dbContext != null)
                {
                     await dbContext.SaveChangesAsync();
                }
               
            }


        }
    }
}
