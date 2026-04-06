using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.BLL.Interfaces;
using RealEstate.DAL.Csv;
using RealEstate.DAL.Persistence;
using RealEstate.DAL.Repositories;

namespace RealEstate.DAL.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRealEstateDal(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

        services.AddScoped<IOwnerRepository, OwnerRepository>();
        services.AddScoped<IApartmentRepository, ApartmentRepository>();
        services.AddScoped<ICsvRecordReader, CsvRecordReader>();
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        return services;
    }
}
