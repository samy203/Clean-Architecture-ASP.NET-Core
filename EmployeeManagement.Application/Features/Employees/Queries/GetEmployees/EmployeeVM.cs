using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Features.Employees.Queries.GetEmployees
{
    public class EmployeeVM
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string Name { get; set; }
        public string PhotoPath { get; set; }
    }
}
