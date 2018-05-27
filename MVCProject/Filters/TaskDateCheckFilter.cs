using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using MVCProject.Contexts;

namespace MVCProject.Filters
{
    public class TaskDateCheckFilter : Attribute, IActionFilter
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
            DateTime dt = DateTime.Now;
            dbContext.TeacherTasks.RemoveRange(dbContext.TeacherTasks.Where(x => x.Deadline != null && Force(x.Deadline) < dt.Date));
            dbContext.SaveChanges();
                        
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
