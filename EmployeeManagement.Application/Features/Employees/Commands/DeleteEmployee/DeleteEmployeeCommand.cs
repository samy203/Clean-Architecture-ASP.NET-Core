using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace EmployeeManagement.Application.Features.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest
    {
        public int EmployeeId { get; set; }
    }
}
