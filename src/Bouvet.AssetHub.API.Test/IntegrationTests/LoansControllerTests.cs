using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Controllers;
using Bouvet.AssetHub.API.Data;
using Bouvet.AssetHub.API.Domain.Asset.Models;
using Bouvet.AssetHub.API.Domain.Asset.Services.Commands;
using Bouvet.AssetHub.API.Domain.Loan.Services.Commands;
using EmptyFiles;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit.Abstractions;

namespace Bouvet.AssetHub.API.Tests
{
    public class LoansControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly CustomWebApplicationFactory _factory;
        private readonly ITestOutputHelper _output;

        public LoansControllerTests(CustomWebApplicationFactory factory, ITestOutputHelper output)
        {

            _factory = factory;
            _httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            _output = output;
        }

        [Fact]
        public async Task GetLoans_Ok_200()
        {
            // Act
            var result = await _httpClient.GetAsync($"api/loans");
            var json = await result.Content.ReadAsStringAsync();
            var loans = JsonSerializer.Deserialize<List<CategoryResponseDto>>(json);

            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.OK);
            loans.ShouldNotBeEmpty();

        }
        [Fact]
        public async Task GetLoanById_Ok_200()
        {
            // Act
            var result = await _httpClient.GetAsync($"api/loans/1");
            var json = await result.Content.ReadAsStringAsync();
            var loan = SerializeHelper.Deserialize<LoanResponseDto>(json);

            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.OK);
            loan.ShouldNotBeNull();
          

        }
        [Fact]
        public async Task GetLoanById_NotFound_404()
        {
            // Act
            var result = await _httpClient.GetAsync($"api/categories/0");

            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);

        }


        [Fact]
        public async Task PostLoan_Ok_200()
        {
            // Arrange 
            var dto = new CreateLoanDto(DateTime.Today, DateTime.Today.AddDays(4), false, 1, 1, "");
            var json = JsonSerializer.Serialize(dto);

            // Act
            var result = await _httpClient.PostAsync($"api/loans", new StringContent(json, Encoding.UTF8, "application/json"));

            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.OK);

        }

        [Fact]
        public async Task PutLoanById_Ok_200()
        {
            // Arrange
            var dto = new UpdateLoanDto(DateTime.Today.AddDays(14));
            var json = SerializeHelper.Serialize(dto);
           
            // Act 
            var result = await _httpClient.PutAsync($"api/loans/2", new StringContent(json, Encoding.UTF8, "application/json"));
            var loan = SerializeHelper.Deserialize<LoanResponseDto>(await result.Content.ReadAsStringAsync());
            
            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.OK);
            loan.IntervalStop.ShouldBe(DateTime.Today.AddDays(14));

        }
       
      
        
        [Fact]
        public async Task DeleteLoanById_Ok_200()
        {
            
            // Act
            var result = await _httpClient.DeleteAsync($"api/loans/2");
            var check = await _httpClient.GetAsync($"api/loans/2");

            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.OK);
            check.StatusCode.ShouldBe(HttpStatusCode.NotFound);

        }
        //[Fact]
        //public async Task DeleteAssetById_BadRequest_400()
        //{
        //    // Act
        //    var result = await _httpClient.DeleteAsync($"api/categories/1");
            
        //    // Assert
        //    result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        //}

    }
}
