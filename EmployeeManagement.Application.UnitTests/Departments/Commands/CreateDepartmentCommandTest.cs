using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Application.Contracts.Persistence;
using EmployeeManagement.Application.Features.Departments.Commands.CreateDepartment;
using EmployeeManagement.Application.Profiles;
using EmployeeManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using Xunit;

namespace EmployeeManagement.Application.UnitTests.Departments.Commands
{
    public class CreateDepartmentCommandTest
    {
        private readonly IMapper mapper;
        private readonly Mock<IDepartmentRepository> mockDepartmentRepository;

        public CreateDepartmentCommandTest()
        {
            mockDepartmentRepository = RepositoryMocks.GetDepartmentRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });

            mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_AddDepartment()
        {
            var handler = new CreateDepartmentCommandHandler(mockDepartmentRepository.Object, mapper);
            
            await handler.Handle(new CreateDepartmentCommand()  { Name = "Finance",Alias = "FN",Description = "Handles Cash Flow"}, CancellationToken.None);

            var allDepartments = await mockDepartmentRepository.Object.ListAllAsync();
            allDepartments.Count.ShouldBe(4);
        }
    }
}
