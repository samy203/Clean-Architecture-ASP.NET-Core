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

namespace EmployeeManagement.Application.Features.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IAsyncRepository<Employee> employeeRepository;
        private readonly IMapper mapper;

        public DeleteEmployeeCommandHandler(IAsyncRepository<Employee> employeeRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employeeToDelete = await employeeRepository.GetByIdAsync(request.EmployeeId);

            if (employeeToDelete == null)
            {
                throw new NotFoundException(nameof(employeeToDelete), request.EmployeeId);
            }

            await employeeRepository.DeleteAsync(employeeToDelete);

            return Unit.Value;
        }
    }
}
