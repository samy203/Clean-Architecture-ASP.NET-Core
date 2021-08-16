using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManagement.Application.Contracts.Persistence;
using EmployeeManagment.Domain.Entities;

namespace EmployeeManagement.Persistence.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeManagementDBContext dbContext) : base(dbContext)
        {
        }
    }
}
