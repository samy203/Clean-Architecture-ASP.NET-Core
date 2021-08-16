using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Application.Contracts.Persistence;
using EmployeeManagement.Application.Features.Departments.Queries.GetDepartments;
using EmployeeManagement.Application.Profiles;
using EmployeeManagement.Application.UnitTests.Mocks;
using EmployeeManagment.Domain.Entities;
using Moq;
using Shouldly;
using Xunit;

namespace EmployeeManagement.Application.UnitTests.Departments.Queries
{
   public class GetDepartmentsQueryTest
    {
        private readonly IMapper mapper;
        private readonly Mock<IDepartmentRepository> mockDepartmentsRepository;

        public GetDepartmentsQueryTest()
        {
            mockDepartmentsRepository = RepositoryMocks.GetDepartmentRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });

            mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetCategoriesListTest()
        {
            var handler = new GetDepartmentsQueryHandler(mockDepartmentsRepository.Object, mapper);

            var result = await handler.Handle(new GetDepartmentsQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<DepartmentVM>>();

            result.Count.ShouldBe(3);
        }
    }
}
