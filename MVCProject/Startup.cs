using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MVCProject.Contexts;
using MVCProject.Models;
using MVCProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace MVCProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            string connection = Configuration.GetConnectionString("TestDb");
            services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(connection));
            services.AddIdentity<Student, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            services.AddTransient<IRepository<Work>, WorkRepository>();

            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddFile("Logs/myapp-{Date}.txt");

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void Index(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await Task.Run(() => context.Response.Redirect("https://google.com"));
            });
        }
    }
}
