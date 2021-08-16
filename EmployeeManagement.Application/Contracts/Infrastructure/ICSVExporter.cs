using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManagement.Application.Features.Employees.Queries.GetEmployeesExport;

namespace EmployeeManagement.Application.Contracts.Infrastructure
{
    public interface ICSVExporter
    {
        byte[] ExportEmployeesToCsv(List<EmployeeExportDTO> employeeExportDtos);
    }
}
