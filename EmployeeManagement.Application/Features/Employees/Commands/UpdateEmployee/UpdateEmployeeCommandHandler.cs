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

namespace EmployeeManagement.Application.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {

        private readonly IAsyncRepository<Employee> employeeRepository;
        private readonly IMapper mapper;

        public UpdateEmployeeCommandHandler(IAsyncRepository<Employee> employeeRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }


        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {

            var employeetoUpdate = await employeeRepository.GetByIdAsync(request.Id);

            if (employeetoUpdate == null)
            {
                throw new NotFoundException(nameof(employeetoUpdate), request.Id);
            }

            var validator = new UpdateEmployeeCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            mapper.Map(request, employeetoUpdate, typeof(UpdateEmployeeCommand), typeof(Employee));

            await employeeRepository.UpdateAsync(employeetoUpdate);

            return Unit.Value;
        }

    }
}
