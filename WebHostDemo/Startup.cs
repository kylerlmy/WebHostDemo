using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace WebHostDemo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IConfiguration configuration, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            lifetime.ApplicationStarted.Register(() =>
            {
                Console.WriteLine("Started");
                Debug.WriteLine("Started");
            });
            lifetime.ApplicationStopped.Register(() =>
            {
                Console.WriteLine("Stopped");
                Debug.WriteLine("Stopped");
            });
            lifetime.ApplicationStopping.Register(() =>
            {
                Console.WriteLine("Stopping");
                Debug.WriteLine("Stopping");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"{configuration["ConnectionStrings:DefaultConnection"]}\r\n");
                await context.Response.WriteAsync($"name= {configuration["name"]}\r\n");
                await context.Response.WriteAsync("**************************************\r\n");

                await context.Response.WriteAsync($"ApplicationName={env.ApplicationName}\r\n");
                await context.Response.WriteAsync($"ContentRootFileProvider={env.ContentRootFileProvider}\r\n");
                await context.Response.WriteAsync($"ContentRootPath={env.ContentRootPath}\r\n");
                await context.Response.WriteAsync($"EnvironmentName={env.EnvironmentName}\r\n");
                await context.Response.WriteAsync($"WebRootFileProvider={env.WebRootFileProvider}\r\n");
                await context.Response.WriteAsync($"WebRootPath={env.WebRootPath}\r\n");
                //await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
