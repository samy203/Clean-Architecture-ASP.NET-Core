using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManagement.Application.Contracts;
using EmployeeManagment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using Xunit;

namespace EmployeeManagement.Persistence.IntegrationTests
{
   public class EmployeeManagementDBContextTests
    {
        private readonly EmployeeManagementDBContext dbContext;
        private readonly Mock<ILoggedInUserService> loggedInUserServiceMock;
        private readonly string loggedInUserId;

        public EmployeeManagementDBContextTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<EmployeeManagementDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            loggedInUserId = "00000000-0000-0000-0000-000000000000";
            loggedInUserServiceMock = new Mock<ILoggedInUserService>();
            loggedInUserServiceMock.Setup(m => m.UserId).Returns(loggedInUserId);

            dbContext = new EmployeeManagementDBContext(dbContextOptions, loggedInUserServiceMock.Object);
        }

        [Fact]
        public async void Save_SetCreatedByProperty()
        {
            var dep = new Department() { Name = "Test Department" };

            dbContext.Departments.Add(dep);
            await dbContext.SaveChangesAsync();

            dep.CreatedBy.ShouldBe(loggedInUserId);
        }
    }
}
