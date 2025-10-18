using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;
using TadbirKish.DataReception.Application.Common.Interfaces;
using TadbirKish.DataReception.Infrastructure.Identity;
using TadbirKish.DataReception.Infrastructure.Persistence;
using TadbirKish.DataReception.Infrastructure.Persistence.Manager;
using TadbirKish.DataReception.Infrastructure.Services;

namespace TadbirKish.DataReception.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<ICacheCoverageManager, CacheCoverageManager>();


            services.AddScoped<ICoverageManager, CoverageManager>();
            services.AddScoped<IRequestCoverageManager, RequestCoverageManager>();


            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<DataReceptionContext>(options =>
                    options.UseInMemoryDatabase("TadbirKish.DataReceptionitectureDb"));
            }
            else
            {
                var address = configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<DataReceptionContext>(options =>
                    options.UseSqlServer(
                        address,
                        sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(DataReceptionContext).Assembly.FullName);
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 1, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null);
                        }),ServiceLifetime.Scoped);
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<DataReceptionContext>());

            services.AddScoped<IDomainEventService, DomainEventService>();

            services
                .AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DataReceptionContext>();

            
            services.AddHostedService<CacheCoverageHostedService>();


            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();



            var identityServerUrl = configuration["IdentityServerUrl"];

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = identityServerUrl;
                    options.RequireHttpsMetadata = false;
                    options.Audience = "IdsSample";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiAccess", policy => policy.RequireAuthenticatedUser());
                options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
            });

            return services;
        }
    }
}