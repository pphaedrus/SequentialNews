


using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NewsCase.Business.Abstract;
using NewsCase.Business.Concrete;
using NewsCase.Business.Mapper;
using NewsCase.Business.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsCase.Business
{
    public static class DependencyInjection
    {
        public static void AddBusiness(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<ISequentialNewsService, SequentialNewsService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));
            services.AddSingleton<IDatabaseSettings>(p =>
            {
                return p.GetRequiredService<IOptions<DatabaseSettings>>().Value;
            });
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        configuration.GetConnectionString("DefaultConnection"),
            //        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            //services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
        }
    }

}
