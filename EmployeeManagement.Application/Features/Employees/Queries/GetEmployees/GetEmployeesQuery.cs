using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace EmployeeManagement.Application.Features.Employees.Queries.GetEmployees
{
    public class GetEmployeesQuery : IRequest<List<EmployeeVM>>
    {
    }
}
