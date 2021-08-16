using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Contracts
{
    public interface ILoggedInUserService
    {
        public string UserId { get; }
    }
}
