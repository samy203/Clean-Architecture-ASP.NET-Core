using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace EmployeeManagement.Application.Features.Employees.Queries.GetEmployeesExport
{
    public class GetEmployeesExportQuery : IRequest<EmployeeExportFileVM>
    {
    }
}
