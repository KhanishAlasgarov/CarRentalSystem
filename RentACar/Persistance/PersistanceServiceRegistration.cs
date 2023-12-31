using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Contexts;
using Persistance.Repositories;

namespace Persistance;

public static class PersistanceServiceRegistration
{
    public static IServiceCollection AddPersistanceService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BaseDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("Local"));
        });

        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IModelRepository, ModelRepository>();
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<ITransmissionRepository, TransmissionRepository>();
        services.AddScoped<IFuelRepository, FuelRepository>();

        return services;
    }
}
