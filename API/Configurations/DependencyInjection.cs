using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.IServices;
using API.Mapper;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(GetConnectionString()));
            return services;
        }

        public static IServiceCollection AddScoped(this IServiceCollection services)
        {
            services.AddScoped<IAuthenService, AuthenService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ILoanService, LoanService>();
            return services;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddAutoMapper(typeof(MapperConfig));
            return services;
        }

        private static string GetConnectionString()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true, true)
                        .Build();
            var strConn = config["ConnectionStrings:DefaultConnection"];

            return strConn ?? "";
        }
    }
}