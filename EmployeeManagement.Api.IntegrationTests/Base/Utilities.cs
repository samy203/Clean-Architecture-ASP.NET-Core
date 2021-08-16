using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManagement.Persistence;
using EmployeeManagment.Domain.Entities;

namespace EmployeeManagement.Api.IntegrationTests.Base
{
    public class Utilities
    {
        public static void InitializeDbForTests(EmployeeManagementDBContext context)
        {
            var sdId = 1;
            var hrId = 2;
            var erpId = 3;

            context.Departments.Add(new Department
            {
                Id = sdId,
                Name = "Software Development",
                Alias = "SD",
                Description = "Responsible of developing new solutions and maintain existing ones"
            });

            context.Departments.Add(new Department
            {
                Id = hrId,
                Name = "Human Resources",
                Alias = "HR",
                Description = "Manage Human Resources"
            });
            context.Departments.Add(new Department
            {
                Id = erpId,
                Name = "Enterprise Resource Planning",
                Alias = "ERP",
                Description = "Responsible of Dealing with the clients"
            });


            context.Employees.Add(new Employee
            {
                Name = "John Egbert",
                Id = 1,
                DepartmentId = sdId,
                Email = "JEgbert@EmployeeManagment.com",
                PhotoPath = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/banjo.jpg",
            });


            context.Employees.Add(new Employee
            {
                Name = "Michael Johnson",
                Id = 2,
                DepartmentId = sdId,
                Email = "MJohnson@EmployeeManagement.com",
                PhotoPath = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/michael.jpg",
            });

            context.Employees.Add(new Employee
            {
                Name = "Sam Jerktron",
                Id = 3,
                DepartmentId = hrId,
                Email = "SJerktron@EmployeeManagement.com",
                PhotoPath = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/dj.jpg",
            });

            context.Employees.Add(new Employee
            {
                Name = "Manuel Santinonisi",
                Id = 4,
                DepartmentId = hrId,
                Email = "MSantinonisi@EmployeeManagement.com",
                PhotoPath = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/guitar.jpg",
            });

            context.Employees.Add(new Employee
            {
                Name = "ManyMony",
                Id = 5,
                DepartmentId = erpId,
                Email = "MMony@EmployeeManagement.com",
                PhotoPath = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/conf.jpg",
            });

            context.Employees.Add(new Employee
            {
                Name = "Nick Sailor",
                Id = 6,
                DepartmentId = erpId,
                Email = "NSailor@EmployeeManagement.com",
                PhotoPath = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/musical.jpg",
            });


            context.SaveChanges();
        }
    }
}
