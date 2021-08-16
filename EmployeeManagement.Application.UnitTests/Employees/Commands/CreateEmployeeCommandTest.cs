using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Application.Contracts.Persistence;
using EmployeeManagement.Application.Features.Departments.Commands.CreateDepartment;
using EmployeeManagement.Application.Features.Employees.Commands.CreateEmployee;
using EmployeeManagement.Application.Profiles;
using EmployeeManagement.Application.UnitTests.Mocks;
using EmployeeManagment.Domain.Entities;
using Moq;
using Shouldly;
using Xunit;

namespace EmployeeManagement.Application.UnitTests.Employees.Commands
{
    public class CreateEmployeeCommandTest
    {

        private readonly IMapper mapper;
        private readonly Mock<IAsyncRepository<Employee>> mockEmployeeRepository;

        public CreateEmployeeCommandTest()
        {
            mockEmployeeRepository = RepositoryMocks.GetEmployeeRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });

            mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_AddEmployee()
        {
            var handler = new CreateEmployeeCommandHandler(mockEmployeeRepository.Object, mapper, null, RepositoryMocks.GetLoggerService().Object);

            await handler.Handle(new CreateEmployeeCommand() { Name = "Scorias", Email = "Scorias@EmployeeManagement.com", DepartmentId = 1 }, CancellationToken.None);

            var allEmployees = await mockEmployeeRepository.Object.ListAllAsync();
            allEmployees.Count.ShouldBe(7);
        }

    }
}
