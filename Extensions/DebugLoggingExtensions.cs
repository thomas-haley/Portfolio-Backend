using System;
using Portfolio_Backend.Data;
using Portfolio_Backend.Interfaces;
using Portfolio_Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace Portfolio_Backend.Extensions;

public static class DebugLoggingExtensions
{
    public static IServiceCollection AddLoggingServices(this IServiceCollection services, IConfiguration config)
    {
       
        services.AddSingleton<ICustomLogger>(
            ctx => 
            {
                return new CustomLogger();
            }
        );

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return services;
    }
}
