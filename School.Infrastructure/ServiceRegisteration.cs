﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace School.Infrastructure
{
    public static class ServiceRegisteration
    {
        public static IServiceCollection AddServiceRegisteration(this IServiceCollection services, IConfiguration configuration)
        {
            return services;

            /*services.AddIdentity<User, Role>(option =>
             {
                 // Password settings.
                 option.Password.RequireDigit = true;
                 option.Password.RequireLowercase = true;
                 option.Password.RequireNonAlphanumeric = true;
                 option.Password.RequireUppercase = true;
                 option.Password.RequiredLength = 6;
                 option.Password.RequiredUniqueChars = 1;

                 // Lockout settings.
                 option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                 option.Lockout.MaxFailedAccessAttempts = 5;
                 option.Lockout.AllowedForNewUsers = true;

                 // User settings.
                 option.User.AllowedUserNameCharacters =
                 "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                 option.User.RequireUniqueEmail = true;
                 option.SignIn.RequireConfirmedEmail = true;

             }).AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();*/
        }
    }
}
