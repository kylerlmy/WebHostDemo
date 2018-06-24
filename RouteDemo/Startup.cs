using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Xsl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;

namespace RouteDemo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();  //添加路由的依赖注入的配置
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //处理HTTP请求时，使用路由（非MVC类）
            //抛错，注意和app.Map("/task")方法的区别
            //app.UseRouter(builder => builder.MapGet("/task", context =>
            //{
            //    return context.Response.WriteAsync(("this ia a ation"));
            //}));

            //UseRouter使用方式1 ，使用RouteHandle来构建
            RequestDelegate handler = context => context.Response.WriteAsync("this is a action2");
            var route = new Route(new RouteHandler(handler), "action2", app.ApplicationServices.GetRequiredService<IInlineConstraintResolver>());

            app.UseRouter(route);

            //UseRouter使用方式2，使用RouteBuilder来构建，route 路由的使用,使用IRouteBuilder接口
            app.UseRouter(builder => builder.MapGet("task", context =>
            {
                return context.Response.WriteAsync(("this ia a ation"));
            }));


            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });

            //Mvc中处理请求时使用Route进行路由，使用IRouteBuilder接口
            app.UseMvc(builder =>
            {
                builder.MapRoute("api/get", context =>
                {
                    return Task.CompletedTask;
                });
                Console.WriteLine("Router");
            });

        }
    }
}
