using EmlakOfisi.BLL.Extensions;
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

namespace EmlakOfisi.AgentUI
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
                opt.LoginPath = new PathString("/User/Login");
                opt.AccessDeniedPath = new PathString("/User/Login");
                opt.Cookie.HttpOnly = true;
                opt.Cookie.Name = "emlkofsCookie";
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
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                            name: "Home",
                            template: "",
                            defaults: new
                            {
                                controller = "Home",
                                action = "Index"
                            });
                routes.MapRoute(
                            name: "RealEstateAds",
                            template: "ilanlar",
                            defaults: new
                            {
                                controller = "RealEstateAd",
                                action = "GetEstateAdList"
                            });
                routes.MapRoute(
                            name: "Login",
                            template: "uyegirisi",
                            defaults: new
                            {
                                controller = "User",
                                action = "Login"
                            });
                routes.MapRoute(
                            name: "ChangePassword",
                            template: "sifre-degistir",
                            defaults: new
                            {
                                controller = "User",
                                action = "ChangePassword"
                            });
                routes.MapRoute(
                             name: "RealEstateAdsByUser",
                             template: "ilanlarim",
                             defaults: new
                             {
                                 controller = "RealEstateAd",
                                 action = "RealEstateAdListByUser"
                             });
                routes.MapRoute(
                             name: "EditRealEstateAds",
                             template: "ilani-duzenle/{id}",
                             defaults: new
                             {
                                 controller = "RealEstateAd",
                                 action = "EditEstateAd"
                                 
                             });
                routes.MapRoute(
                            name: "AddRealEstateAds",
                            template: "ilan-ekle",
                            defaults: new
                            {
                                controller = "RealEstateAd",
                                action = "AddEstateAd"

                            });

                routes.MapRoute(
                 name: "default",
                 template: "{controller=Home}/{action=Index}/{id?}"
                 );
            });
        }
    }
}
