using MediatR;

namespace EmployeeManagement.Application.Features.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommand : IRequest<CreateDepartmentCommandResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Alias { get; set; }
    }
}
