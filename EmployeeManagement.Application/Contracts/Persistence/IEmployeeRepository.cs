using EmployeeManagment.Domain.Entities;

namespace EmployeeManagement.Application.Contracts.Persistence
{
    public interface IEmployeeRepository : IAsyncRepository<Employee>
    {
    }
}
