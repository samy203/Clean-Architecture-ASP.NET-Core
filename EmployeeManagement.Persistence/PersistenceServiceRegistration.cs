using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManagement.Application.Contracts.Persistence;
using EmployeeManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<EmployeeManagementDBContext>(o =>
                o.UseSqlServer(configuration.GetConnectionString("EmployeeManagementCleanArchConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IEmployeeRepository), typeof(EmployeeRepository));
            services.AddScoped(typeof(IDepartmentRepository), typeof(DepartmentRepository));

            return services;
        }
    }
}
