using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Features.Employees.Queries.GetEmployeeDetail
{
    public class EmployeeDetailsVM
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhotoPath { get; set; }
        public DepartmentDTO Department { get; set; }
    }
}
