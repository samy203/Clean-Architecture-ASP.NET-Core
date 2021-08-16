using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Features.Employees.Queries.GetEmployeesExport
{
    public class EmployeeExportFileVM
    {
        public string EmployeeExportFileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
