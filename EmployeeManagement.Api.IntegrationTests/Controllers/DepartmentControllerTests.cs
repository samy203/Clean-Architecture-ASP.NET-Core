using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.Api.IntegrationTests.Base;
using EmployeeManagement.Application.Features.Departments.Queries.GetDepartments;
using Newtonsoft.Json;
using Xunit;

namespace EmployeeManagement.Api.IntegrationTests.Controllers
{
    public class DepartmentControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;

        public DepartmentControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task ReturnsSuccessResult()
        {
            var client = factory.GetAnonymousClient();

            var response = await client.GetAsync("/api/department/all");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<DepartmentVM>>(responseString);

            Assert.IsType<List<DepartmentVM>>(result);
            Assert.NotEmpty(result);
        }
    }
}
