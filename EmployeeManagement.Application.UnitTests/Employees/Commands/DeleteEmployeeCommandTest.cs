using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Application.Contracts.Persistence;
using EmployeeManagement.Application.Features.Employees.Commands.CreateEmployee;
using EmployeeManagement.Application.Features.Employees.Commands.DeleteEmployee;
using EmployeeManagement.Application.Profiles;
using EmployeeManagement.Application.UnitTests.Mocks;
using EmployeeManagment.Domain.Entities;
using Moq;
using Shouldly;
using Xunit;

namespace EmployeeManagement.Application.UnitTests.Employees.Commands
{
    public class DeleteEmployeeCommandTest
    {
        private readonly IMapper mapper;
        private readonly Mock<IAsyncRepository<Employee>> mockEmployeeRepository;

        public DeleteEmployeeCommandTest()
        {
            mockEmployeeRepository = RepositoryMocks.GetEmployeeRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });

            mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_DeleteEmployee()
        {
            var handler = new DeleteEmployeeCommandHandler(mockEmployeeRepository.Object, mapper);

            await handler.Handle(new DeleteEmployeeCommand() { EmployeeId = 1 }, CancellationToken.None);

            var allEmployees = await mockEmployeeRepository.Object.ListAllAsync();
            allEmployees.Count.ShouldBe(5);
        }
    }
}
