using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManagment.Domain.Entities;
using MediatR;

namespace EmployeeManagement.Application.Features.Departments.Queries.GetDepartments
{
    public class DepartmentVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
    }
}
