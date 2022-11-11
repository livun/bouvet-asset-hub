using Bouvet.AssetHub.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit.Abstractions;

namespace Bouvet.AssetHub.API.Tests
{
    public class LoanHistoryControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly CustomWebApplicationFactory _factory;
        private readonly ITestOutputHelper _output;

        public LoanHistoryControllerTests(CustomWebApplicationFactory factory, ITestOutputHelper output)
        {

            _factory = factory;
            _httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            _output = output;
        }

        [Fact]
        public async Task GetLoanHistory_Ok_200()
        {
            // Arrange 
                // Create loan
            var dto = new CreateLoanDto( DateTime.Today, DateTime.Today.AddDays(4), false, 2, 3,"");
            var postJson = JsonSerializer.Serialize(dto);
            var response = await _httpClient.PostAsync($"api/loans", new StringContent(postJson, Encoding.UTF8, "application/json"));
            var loan = SerializeHelper.Deserialize<LoanResponseDto>(await response.Content.ReadAsStringAsync());
            
                // Delete loan - will create a new loanhistory in backend
            await _httpClient.DeleteAsync($"api/loans/{loan.Id}");

            // Act
            var result = await _httpClient.GetAsync($"api/loanhistory");
            var json = await result.Content.ReadAsStringAsync();
            var loans = JsonSerializer.Deserialize<List<LoanHistoryResponseDto>>(json);
            
            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.OK);
            loans.ShouldNotBeEmpty();

        }





    }
}
