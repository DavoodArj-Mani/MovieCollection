using System;
using Microsoft.Extensions.DependencyInjection;
using MovieCollection.Services.App.AuthenticationServices;
using MovieCollection.Services.App.RoleServices;
using MovieCollection.Services.App.UserServices;
using MovieCollection.Services.Core.CollectionServices;
using MovieCollection.Services.Core.MovieServices;

namespace MovieCollection.Model
{
    public static class DependencyInjectionRegistry
    {
        public static IServiceCollection AddMyServices(this IServiceCollection services)
        {
            //services.AddTransient<IAuthenticationService, AuthenticationService>();
            //services.AddScoped<ILoggingService, LoggingService>();
            //services.AddSingleton<ICacheProvider, CacheProvider>();


            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<ICollectionService, CollectionService>();


            return services;
        }
    }
}
