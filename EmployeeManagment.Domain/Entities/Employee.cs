using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManagment.Domain.Common;

namespace EmployeeManagment.Domain.Entities
{
    public class Employee : AuditableEntity
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Department Department { get; set; }
        public string PhotoPath { get; set; }
    }
}
