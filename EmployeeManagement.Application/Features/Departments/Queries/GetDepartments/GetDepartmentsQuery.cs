using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace EmployeeManagement.Application.Features.Departments.Queries.GetDepartments
{
    public class GetDepartmentsQuery : IRequest<List<DepartmentVM>>
    {
    }
}
