using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Application.Contracts.Persistence;
using EmployeeManagement.Application.Features.Employees.Queries.GetEmployeeDetail;
using EmployeeManagement.Application.Features.Employees.Queries.GetEmployees;
using EmployeeManagement.Application.Profiles;
using EmployeeManagement.Application.UnitTests.Mocks;
using EmployeeManagment.Domain.Entities;
using Moq;
using Shouldly;
using Xunit;

namespace EmployeeManagement.Application.UnitTests.Employees.Queries
{
    public class GetEmployeeDetailsQueryTest
    {
        private readonly IMapper mapper;
        private readonly Mock<IAsyncRepository<Employee>> mockEmployeeRepository;
        private readonly Mock<IDepartmentRepository> mockDepartmentRepository;

        public GetEmployeeDetailsQueryTest()
        {
            mockEmployeeRepository = RepositoryMocks.GetEmployeeRepository();
            mockDepartmentRepository = RepositoryMocks.GetDepartmentRepository();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });

            mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_GetEmployees()
        {
            var handler = new GetEmployeeDetailsQueryHandler(mockEmployeeRepository.Object, mockDepartmentRepository.Object, mapper);

            var result = await handler.Handle(new GetEmployeeDetailsQuery() { Id = 1 }, CancellationToken.None);

            var vm = new EmployeeDetailsVM()
            {
                Department = new DepartmentDTO()
                {
                    Id = 1,
                    Name = "Software Development",
                    Alias = "SD"
                },
                DepartmentId = 1,
                Email = "JEgbert@EmployeeManagment.com",
                Id = 1,
                Name = "John Egbert",
                PhotoPath = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/banjo.jpg"
            };

            result.ShouldBeOfType<EmployeeDetailsVM>();

            result.ShouldBeEquivalentTo(vm);
        }
    }
}
