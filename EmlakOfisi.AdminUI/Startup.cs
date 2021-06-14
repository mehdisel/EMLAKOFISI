using EmlakOfisi.BLL.Extensions;
using EmlakOfisi.DAL.Concrete.EntityFramework.Contexts;
using EmlakOfisi.Entities.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmlakOfisi.AdminUI
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
            services.AddCustomDbContext(Configuration);
            services.AddCustomIdentity();
            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Admin/User/Login");
                opt.AccessDeniedPath = new PathString("/Admin/User/Login");
                opt.Cookie.HttpOnly = true;
                opt.Cookie.Name = "emlkofsadminCookie";
                opt.Cookie.SameSite = SameSiteMode.Strict;
                opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                opt.ExpireTimeSpan = TimeSpan.FromDays(20);
            });
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddControllersWithViews();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            

            app.UseAuthentication();

            app.UseAuthorization();
            app.UseRouting();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                            name: "default",
                            template: "Admin/{controller=User}/{action=Index}/{id?}"
                            );

                routes.MapRoute(
                            name: "Home",
                            template: "",
                            defaults: new
                            {
                                controller = "User",
                                action = "Login"
                            });
                routes.MapRoute(
                    name: "Register",
                    template: "AddCompany",
                    defaults: new
                    {
                        controller = "User",
                        action = "Register"
                    });
            });



        }
    }
}
