using CleanCode.Core.Application.Contracts.Persistence;
using CleanCode.Infrastructure.Persistence.DatabaseContext;
using CleanCode.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanCode.Infrastructure.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContextImpl>(opts =>
        {
            opts.UseSqlServer(configuration.GetConnectionString("LeaveConnectionString"));
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepositoryImpl<>));
        services.AddScoped<ILeaveTypeRepository, LeaveTypeRepositoryImpl>();
        services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepositoryImpl>();
        services.AddScoped<ILeaveRequestRepository, LeaveRequestRepositoryImpl>();

        return services;
    }
}
