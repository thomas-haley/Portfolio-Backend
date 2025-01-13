using System;
using Portfolio_Backend.Data;
using Portfolio_Backend.Interfaces;
using Portfolio_Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace Portfolio_Backend.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();
        services.AddDbContext<DataContext>(opt => {
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });
        services.AddCors();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IContentRepository, ContentRepository>();
        services.AddScoped<IContentListRepository, ContentListRepository>();
        services.AddScoped<IContentListContentRepository, ContentListContentRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();


        // services.AddSingleton<IAPIQueue>(
        //     ctx => 
        //     {
        //         if(!int.TryParse(config["QueueCapacity"], out var queueCapacity))
        //             queueCapacity = 10;
        //         return new APIQueue(queueCapacity);
        //     }
        // );

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return services;
    }
}
