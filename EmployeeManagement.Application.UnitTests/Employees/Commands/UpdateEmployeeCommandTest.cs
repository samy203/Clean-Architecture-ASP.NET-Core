using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Application.Contracts.Persistence;
using EmployeeManagement.Application.Features.Employees.Commands.CreateEmployee;
using EmployeeManagement.Application.Features.Employees.Commands.UpdateEmployee;
using EmployeeManagement.Application.Profiles;
using EmployeeManagement.Application.UnitTests.Mocks;
using EmployeeManagment.Domain.Entities;
using Moq;
using Shouldly;
using Xunit;

namespace EmployeeManagement.Application.UnitTests.Employees.Commands
{
    public class UpdateEmployeeCommandTest
    {
        private readonly IMapper mapper;
        private readonly Mock<IAsyncRepository<Employee>> mockEmployeeRepository;

        public UpdateEmployeeCommandTest()
        {
            mockEmployeeRepository = RepositoryMocks.GetEmployeeRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });

            mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_UpdateEmployee()
        {
            var handler = new UpdateEmployeeCommandHandler(mockEmployeeRepository.Object, mapper);

            var emp = new Employee()
            { Id = 1, Name = "Scorias", Email = "Scorias@EmployeeManagement.com", DepartmentId = 2 };

            await handler.Handle(new UpdateEmployeeCommand() { Id = 1, Name = "Scorias", Email = "Scorias@EmployeeManagement.com", DepartmentId = 2 }, CancellationToken.None);

            var allEmployees = await mockEmployeeRepository.Object.ListAllAsync();
            var updatedEmployee = allEmployees.FirstOrDefault(e => e.Id == 1);

            updatedEmployee.ShouldBeEquivalentTo(emp);
        }

    }
}
