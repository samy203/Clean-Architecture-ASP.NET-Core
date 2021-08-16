using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.Application.Contracts.Persistence;
using EmployeeManagment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Persistence.Repositories
{
   public class DepartmentRepository:BaseRepository<Department>,IDepartmentRepository
    {
        public DepartmentRepository(EmployeeManagementDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Department>> GetDepartmentsWithEmployees()
        {
            return  await context.Departments.Include(x => x.Employees).ToListAsync();
        }

        public Task<bool> IsDepartmentNameUnique(string name)
        {
            var matches = context.Departments.Any(e => e.Name.Equals(name));
            return Task.FromResult(matches);
        }

        public Task<bool> IsDepartmentAliasUnique(string alias)
        {
            var matches = context.Departments.Any(e => e.Alias.Equals(alias));
            return Task.FromResult(matches);
        }
    }
}
