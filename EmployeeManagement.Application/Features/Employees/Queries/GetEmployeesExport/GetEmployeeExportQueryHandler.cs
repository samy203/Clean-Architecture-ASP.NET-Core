using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Application.Contracts.Infrastructure;
using EmployeeManagement.Application.Contracts.Persistence;
using EmployeeManagment.Domain.Entities;
using MediatR;

namespace EmployeeManagement.Application.Features.Employees.Queries.GetEmployeesExport
{
    public class GetEmployeeExportQueryHandler : IRequestHandler<GetEmployeesExportQuery, EmployeeExportFileVM>
    {
        private readonly IAsyncRepository<Employee> employeeRepository;
        private readonly IMapper mapper;
        private readonly ICSVExporter csvExporter;

        public GetEmployeeExportQueryHandler(IMapper mapper, IAsyncRepository<Employee> repo, ICSVExporter csvExporter)
        {
            this.mapper = mapper;
            employeeRepository = repo;
            this.csvExporter = csvExporter;
        }

        public async Task<EmployeeExportFileVM> Handle(GetEmployeesExportQuery request, CancellationToken cancellationToken)
        {
            var employees = mapper.Map<List<EmployeeExportDTO>>((await employeeRepository.ListAllAsync()).OrderBy(x => x.Department).ThenBy(x => x.Name));

            var fileData = csvExporter.ExportEmployeesToCsv(employees);

            var employeesExportFileDto = new EmployeeExportFileVM() { ContentType = "text/csv", Data = fileData, EmployeeExportFileName = $"{Guid.NewGuid()}.csv" };

            return employeesExportFileDto;
        }
    }
}
