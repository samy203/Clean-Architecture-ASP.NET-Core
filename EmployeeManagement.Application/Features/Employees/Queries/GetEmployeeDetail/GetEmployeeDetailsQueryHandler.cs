using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Application.Contracts.Persistence;
using EmployeeManagement.Application.Exceptions;
using EmployeeManagment.Domain.Entities;
using MediatR;

namespace EmployeeManagement.Application.Features.Employees.Queries.GetEmployeeDetail
{
    public class GetEmployeeDetailsQueryHandler : IRequestHandler<GetEmployeeDetailsQuery, EmployeeDetailsVM>
    {
        private readonly IAsyncRepository<Employee> employeeRepository;
        private readonly IAsyncRepository<Department> departmentRepository;
        private readonly IMapper _mapper;

        public GetEmployeeDetailsQueryHandler(IAsyncRepository<Employee> employeeRepository, IAsyncRepository<Department> departmentRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeDetailsVM> Handle(GetEmployeeDetailsQuery request, CancellationToken cancellationToken)
        {
            var employee = await employeeRepository.GetByIdAsync(request.Id);
            var employeeVM = _mapper.Map<EmployeeDetailsVM>(employee);

            var department = await departmentRepository.GetByIdAsync(employee.DepartmentId);

            if (department == null)
            {
                throw new NotFoundException(nameof(Employee), request.Id);
            }
            employeeVM.Department = _mapper.Map<DepartmentDTO>(department);

            return employeeVM;
        }
    }
}
