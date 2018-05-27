using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using MVCProject.Contexts;

namespace MVCProject.Filters
{
    public class ReviewCheckFilter : Attribute, IActionFilter
    {
        public DateTime Force(string task_date)
        {
            var arr = task_date.Split('.');
            int day = Convert.ToInt32(arr[0]);
            int month = Convert.ToInt32(arr[1]);
            int year = Convert.ToInt32(arr[2]);
            return new DateTime(year, month, day);
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var services = context.HttpContext.RequestServices;
            var dbContext = services.GetService(typeof(AppDbContext)) as AppDbContext;

            var userId = context.HttpContext.User.Claims.FirstOrDefault()?.Value;
            //var tasks = dbContext.TeacherTasks.Where(x => x.TeacherId == userId).ToList();
            int id = (int)context.ActionArguments["id"];
            var work = dbContext.Works.Where(x => x.Id == id).FirstOrDefault();
            int disc_id = work.DisciplineId;
            var task = dbContext.TeacherTasks.Where(x => x.Id == disc_id).FirstOrDefault();
            if(task == null)
            {
                context.HttpContext.Response.Redirect(context.HttpContext.Request.PathBase + "/Alert/TaskEnd");
            }

        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
