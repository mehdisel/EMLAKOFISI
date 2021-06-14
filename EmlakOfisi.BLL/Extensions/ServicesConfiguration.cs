using EmlakOfisi.DAL.Concrete.EntityFramework.Contexts;
using EmlakOfisi.Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakOfisi.BLL.Extensions
{
    public static class  ServicesConfiguration
    {
        public static void AddCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options=>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireNonAlphanumeric = false;
                

            }).AddEntityFrameworkStores<EmlakOfisiContext>();
        }
        public static void AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EmlakOfisiContext>();
        }
    }
}
