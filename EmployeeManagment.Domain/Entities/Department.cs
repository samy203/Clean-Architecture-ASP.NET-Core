using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManagment.Domain.Common;

namespace EmployeeManagment.Domain.Entities
{
    public class Department : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
