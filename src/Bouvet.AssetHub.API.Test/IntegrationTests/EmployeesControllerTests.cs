using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Controllers;
using Bouvet.AssetHub.API.Data;
using Bouvet.AssetHub.API.Domain.Asset.Models;
using Bouvet.AssetHub.API.Domain.Asset.Services.Commands;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit.Abstractions;

namespace Bouvet.AssetHub.API.Tests
{
    public class EmployeesControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly CustomWebApplicationFactory _factory;
        private readonly ITestOutputHelper _output;

        public EmployeesControllerTests(CustomWebApplicationFactory factory, ITestOutputHelper output)
        {

            _factory = factory;
            _httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            _output = output;
        }

        [Fact]
        public async Task GetLoansByEmployeeNumber_Ok_200()
        {
            // Act
            var result = await _httpClient.GetAsync($"api/employees/3456/loans");
            var json = await result.Content.ReadAsStringAsync();
            var assets = JsonSerializer.Deserialize<List<LoanResponseDto>>(json);
                       

            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.OK);
            assets.ShouldNotBeEmpty();

        }
        [Fact]
        public async Task GetLoansByEmployeeNumber_NotFound_404()
        {
            // Act
            var result = await _httpClient.GetAsync($"api/employees/6734/loans");
           
            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);

        }

    }
}
