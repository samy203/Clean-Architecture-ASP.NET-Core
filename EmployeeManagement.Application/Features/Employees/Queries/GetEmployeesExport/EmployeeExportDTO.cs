using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Features.Employees.Queries.GetEmployeesExport
{
    public class EmployeeExportDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DepartmentName { get; set; }
        public string Email { get; set; }
    }
}
