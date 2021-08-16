using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.Application.Contracts.Infrastructure;
using EmployeeManagement.Application.Contracts.Persistence;
using EmployeeManagement.Application.Features.Employees.Commands.CreateEmployee;
using EmployeeManagment.Domain.Entities;
using EmptyFiles;
using Microsoft.Extensions.Logging;
using Moq;
using ILogger = Castle.Core.Logging.ILogger;

namespace EmployeeManagement.Application.UnitTests.Mocks
{
    public class RepositoryMocks
    {
        public static Mock<IDepartmentRepository> GetDepartmentRepository()
        {
            var departments = new List<Department>
            {
                new Department()
                {
                    Id = 1,
                    Name = "Software Development",
                    Alias = "SD",
                    Description = "Responsible of developing new solutions and maintain existing ones"
                },
                new Department()
                {
                    Id = 2,
                    Name = "Human Resources",
                    Alias = "HR",
                    Description = "Manage Human Resources"
                },
                new Department()
                {
                    Id = 3,
                    Name = "Enterprise Resource Planning",
                    Alias = "ERP",
                    Description = "Responsible of Dealing with the clients"
                }
            };


            var mockDepartmentRepository = new Mock<IDepartmentRepository>();

            mockDepartmentRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(departments);

            mockDepartmentRepository.Setup(repo => repo.AddAsync(It.IsAny<Department>())).ReturnsAsync(
                (Department department) =>
                {
                    departments.Add(department);
                    return department;
                });

            mockDepartmentRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    return departments.Find(e => e.Id == id);
                });


            return mockDepartmentRepository;
        }


        public static Mock<IAsyncRepository<Employee>> GetEmployeeRepository()
        {
            var employees = new List<Employee>
            {
                new Employee()
                {
                    Name = "John Egbert",
                    Id = 1,
                    DepartmentId = 1,
                    Email = "JEgbert@EmployeeManagment.com",
                    PhotoPath = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/banjo.jpg",
                },

                new Employee()
                {
                    Name = "Michael Johnson",
                    Id = 2,
                    DepartmentId = 1,
                    Email = "MJohnson@EmployeeManagement.com",
                    PhotoPath = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/michael.jpg",
                },

                new Employee()
                {
                    Name = "Sam Jerktron",
                    Id = 3,
                    DepartmentId = 2,
                    Email = "SJerktron@EmployeeManagement.com",
                    PhotoPath = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/dj.jpg",
                },

                new Employee()
                {
                    Name = "Manuel Santinonisi",
                    Id = 4,
                    DepartmentId = 2,
                    Email = "MSantinonisi@EmployeeManagement.com",
                    PhotoPath = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/guitar.jpg",
                },

                new Employee()
                {
                    Name = "ManyMony",
                    Id = 5,
                    DepartmentId = 3,
                    Email = "MMony@EmployeeManagement.com",
                    PhotoPath = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/conf.jpg",
                },

                new Employee()
                {
                    Name = "Nick Sailor",
                    Id = 6,
                    DepartmentId = 3,
                    Email = "NSailor@EmployeeManagement.com",
                    PhotoPath = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/musical.jpg",
                },
            };


            var mockEmployeeRepository = new Mock<IAsyncRepository<Employee>>();

            mockEmployeeRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(employees);

            mockEmployeeRepository.Setup(repo => repo.AddAsync(It.IsAny<Employee>())).ReturnsAsync(
                (Employee employee) =>
                {
                    employees.Add(employee);
                    return employee;
                });

            mockEmployeeRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Employee>())).Returns(
                (Employee employee) =>
                {
                    employees.Remove(employee);
                    return Task.CompletedTask;
                });

            mockEmployeeRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Employee>())).Returns(
                (Employee updatedEmployee) =>
                {
                    var matched = employees.Find(e => e.Id == updatedEmployee.Id);
                    if (matched != null) matched = updatedEmployee;
                    return Task.CompletedTask;
                });

            mockEmployeeRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    return employees.Find(e => e.Id == id);
                });


            return mockEmployeeRepository;
        }

        public static Mock<ILogger<CreateEmployeeCommandHandler>> GetLoggerService()
        {
            var mockLogger = new Mock<ILogger<CreateEmployeeCommandHandler>>();

            return mockLogger;
        }

    }
}