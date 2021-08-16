using EmployeeManagement.Application.Response;

namespace EmployeeManagement.Application.Features.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommandResponse : BaseResponse
    {
        public CreateDepartmentDTO Department { get; set; }
    }
}
