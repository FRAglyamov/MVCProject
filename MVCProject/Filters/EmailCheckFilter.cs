using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using MVCProject.Contexts;

namespace MVCProject.Filters
{
    public class EmailCheckFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var services = context.HttpContext.RequestServices;
            var dbContext = services.GetService(typeof(AppDbContext)) as AppDbContext;

            var userId = context.HttpContext.User.Claims.FirstOrDefault()?.Value;

            var isConfirmed = false;
            isConfirmed = dbContext.Users.Find(userId).EmailConfirmed;
            if(!isConfirmed)
            {
                context.HttpContext.Response.Redirect(context.HttpContext.Request.PathBase + "/Alert/EmailNotConfirmed");
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
