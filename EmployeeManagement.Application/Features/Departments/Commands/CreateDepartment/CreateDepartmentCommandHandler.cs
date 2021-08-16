using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Application.Contracts.Persistence;
using EmployeeManagment.Domain.Entities;
using MediatR;

namespace EmployeeManagement.Application.Features.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, CreateDepartmentCommandResponse>
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly IMapper mapper;

        public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            this.departmentRepository = departmentRepository;
            this.mapper = mapper;
        }


        public async Task<CreateDepartmentCommandResponse> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateDepartmentCommandResponse();

            var validator = new CreateDepartmentCommandValidator(departmentRepository);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.Errors.Count > 0)
            {
                response.Success = false;
                response.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    response.ValidationErrors.Add(error.ErrorMessage);
                }
            }

            if (response.Success)
            {
                var department = new Department() { Name = request.Name, Alias = request.Alias, Description = request.Description };
                department = await departmentRepository.AddAsync(department);
                response.Department = mapper.Map<CreateDepartmentDTO>(department);
            }

            return response;
        }
    }
}
