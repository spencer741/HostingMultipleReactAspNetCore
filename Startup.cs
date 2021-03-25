using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using System.Data;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;
using System.Security.Principal;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace HostingMultipleReactAspNetCore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
   
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.Map("/spa1", spa1 =>
            {
                if (env.IsDevelopment())
                {
                    spa1.UseSpa(spa =>
                    {         
                        spa.Options.SourcePath = Path.Join(env.ContentRootPath, "spa1");
                        spa.UseProxyToSpaDevelopmentServer("http://localhost:3001/");
                    });
                }
            });

            app.Map("/spa2", spa2 =>
            {
                if (env.IsDevelopment())
                {
                    spa2.UseSpa(spa =>
                    {         
                        spa.Options.SourcePath = Path.Join(env.ContentRootPath, "spa2");
                        spa.UseProxyToSpaDevelopmentServer("http://localhost:3002/");
                    });
                }
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
