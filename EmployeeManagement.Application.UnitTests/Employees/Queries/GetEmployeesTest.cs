using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Application.Contracts.Persistence;
using EmployeeManagement.Application.Features.Departments.Queries.GetDepartments;
using EmployeeManagement.Application.Features.Employees.Commands.UpdateEmployee;
using EmployeeManagement.Application.Features.Employees.Queries.GetEmployees;
using EmployeeManagement.Application.Profiles;
using EmployeeManagement.Application.UnitTests.Mocks;
using EmployeeManagment.Domain.Entities;
using Moq;
using Shouldly;
using Xunit;

namespace EmployeeManagement.Application.UnitTests.Employees.Queries
{
    public class GetEmployeesTest
    {
        private readonly IMapper mapper;
        private readonly Mock<IAsyncRepository<Employee>> mockEmployeeRepository;

        public GetEmployeesTest()
        {
            mockEmployeeRepository = RepositoryMocks.GetEmployeeRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });

            mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_GetEmployees()
        {
            var handler = new GetEmployeesQueryHandler(mockEmployeeRepository.Object, mapper);

            var result = await handler.Handle(new GetEmployeesQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<EmployeeVM>>();

            result.Count.ShouldBe(6);
        }
    }
}
