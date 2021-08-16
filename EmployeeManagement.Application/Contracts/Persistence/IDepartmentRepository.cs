using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagment.Domain.Entities;

namespace EmployeeManagement.Application.Contracts.Persistence
{
    public interface IDepartmentRepository : IAsyncRepository<Department>
    {
        Task<List<Department>> GetDepartmentsWithEmployees();
        Task<bool> IsDepartmentNameUnique(string name);
        Task<bool> IsDepartmentAliasUnique(string alias);
    }
}
