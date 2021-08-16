using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManagement.Application.Contracts.Infrastructure;
using EmployeeManagement.Application.Models.Mail;
using EmployeeManagement.Infrastructure.FileExport;
using EmployeeManagement.Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            
            services.AddTransient<ICSVExporter, CSVExporter>();
            services.AddTransient<IEmailService, EMailService>();

            return services;
        }
    }
}
