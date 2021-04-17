using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PlantListing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using PlantListing.Infrastructure;
using PlantListing.Infrastructure.Filters;

namespace PlantListing.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomMVC(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PlantListing",
                    Version = "v1"
                });
            });

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var builder = new SqlConnectionStringBuilder(configuration.GetConnectionString("PlantListingDB"))
            {
                UserID = configuration["DBUser"],
                Password = configuration["DBPassword"]
            };

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<PlantListingContext>(options =>
                {
                    options.UseSqlServer(builder.ConnectionString,
                                         sqlServerOptionsAction: sqlOptions =>
                                         {
                                             sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                             sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                         });
                });

            return services;
        }
    }
}
