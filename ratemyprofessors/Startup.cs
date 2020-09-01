using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ratemyprofessors.Models;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Globalization;

namespace ratemyprofessors
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
            services.AddDbContext<DataBaseContext>(options =>
                 options.UseSqlite("Data Source=app.db"));

            services.AddSingleton<ProfessorCache>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.MapWhen(context => context.Request.GetDisplayUrl().Contains("/Admin/Accounts", StringComparison.InvariantCultureIgnoreCase), c =>
            {
                c.Use(async (http, next) =>
                {
                    if (http.Request.Cookies.ContainsKey("Admin"))
                    {
                        var tok = http.Request.Cookies["Admin"];
                        if (!Guid.TryParse(tok, out var Tok))
                        {
                            http.Response.Redirect("/AdminPannel");
                            return;
                        }
                        if (ratemyprofessors.Pages.AdminPannelModel.AdminTokens.ContainsKey(Tok))
                        {
                            if (ratemyprofessors.Pages.AdminPannelModel.AdminTokens[Tok] == "Hamed")
                            {
                                await next.Invoke();
                            }
                            else
                            {
                                http.Response.Redirect("/AdminPannel");
                                return;
                            }
                        }
                        else
                        {
                            http.Response.Redirect("/AdminPannel");
                            return;
                        }
                    }
                    else
                    {
                        http.Response.Redirect("/AdminPannel");
                        return;
                    }

                });
                c.UseStaticFiles();
                c.UseSpaStaticFiles();

                c.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller}/{action=Index}/{id?}");
                });
            });
            app.MapWhen(context => context.Request.GetDisplayUrl().Contains("/Admin/", StringComparison.InvariantCultureIgnoreCase), c =>
             {
                 c.Use(async (http, next) =>
                 {
                     if (http.Request.Cookies.ContainsKey("Admin"))
                     {
                         var tok = http.Request.Cookies["Admin"];
                         if (!Guid.TryParse(tok, out var Tok))
                         {
                             http.Response.Redirect("/AdminPannel");
                             return;
                         }
                         if (ratemyprofessors.Pages.AdminPannelModel.AdminTokens.ContainsKey(Tok))
                         {
                             await next.Invoke();
                         }
                         else
                         {
                             http.Response.Redirect("/AdminPannel");
                             return;
                         }
                     }
                     else
                     {
                         http.Response.Redirect("/AdminPannel");
                         return;
                     }

                 });
                 c.UseStaticFiles();

                 c.UseMvc(routes =>
                 {
                     routes.MapRoute(
                         name: "default",
                         template: "{controller}/{action=Index}/{id?}");
                 });
             });
            app.MapWhen(context => context.Request.GetDisplayUrl().Contains("/email", StringComparison.InvariantCultureIgnoreCase), c =>
            {
                c.UseMvc();
            });
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
