using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace EmployeeManagement.Application.Features.Employees.Queries.GetEmployeeDetail
{
    public class GetEmployeeDetailsQuery : IRequest<EmployeeDetailsVM>
    {
        public int Id { get; set; }
    }
}
