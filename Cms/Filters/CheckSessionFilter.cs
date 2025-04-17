using Cms.Attributes;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace Cms.Filters
{
    public class CheckSessionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controllActionDesc = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllActionDesc == null)
            {
                return;
            }
            var noCheck = controllActionDesc.MethodInfo.GetCustomAttribute<NoCheckAttribute>(false);
            if (noCheck != null)
            {
                await next();
            }
            else
            {
                if (!string.IsNullOrEmpty(context.HttpContext.Session.GetString("user")))
                {
                    await next();
                }
                else
                {
                    context.HttpContext.Response.Redirect("/login/index?returnUrl="+context.HttpContext.Request.Path.ToString());
                }
            }


        }
    }
}
