using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MiddleWare
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //这里的IApplicationBuilder(appTask),与构造函数注入的IApplicationBuilder（app）不是同一个实例
            app.Map("/task", apptask =>
            {
                apptask.Run(async context =>
                {
                    await context.Response.WriteAsync("this is a task");
                });
            });




            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("1:before start .....\r\n");
                await next.Invoke();
            });

            app.Use(next =>
            {
                return (context) =>
                {
                    return context.Response.WriteAsync("2:in the middle of start .....\r\n");
                    // return next(context);
                };
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("3:Start ...\r\n");
            });


          
        }
    }
}
