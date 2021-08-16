using AutoMapper;
using EmployeeManagement.Application.Contracts.Persistence;
using EmployeeManagment.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Features.Employees.Queries.GetEmployees
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<EmployeeVM>>
    {

        private readonly IAsyncRepository<Employee> employeeRepository;
        private readonly IMapper mapper;

        public GetEmployeesQueryHandler(IAsyncRepository<Employee> employeeRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }

        public async Task<List<EmployeeVM>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = (await employeeRepository.ListAllAsync()).OrderBy(x => x.Name);
            return mapper.Map<List<EmployeeVM>>(employees);
        }
    }
}
