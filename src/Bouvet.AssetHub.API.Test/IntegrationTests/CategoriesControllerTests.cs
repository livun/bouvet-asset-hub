using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Controllers;
using Bouvet.AssetHub.API.Data;
using Bouvet.AssetHub.API.Domain.Asset.Models;
using Bouvet.AssetHub.API.Domain.Asset.Services.Commands;
using EmptyFiles;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit.Abstractions;

namespace Bouvet.AssetHub.API.Tests
{
    public class CategoriesControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly CustomWebApplicationFactory _factory;
        private readonly ITestOutputHelper _output;

        public CategoriesControllerTests(CustomWebApplicationFactory factory, ITestOutputHelper output)
        {

            _factory = factory;
            _httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            _output = output;
        }

        [Fact]
        public async Task GetCategories_Ok_200()
        {
            // Act
            var result = await _httpClient.GetAsync($"api/categories");
            var json = await result.Content.ReadAsStringAsync();
            var categories = JsonSerializer.Deserialize<List<CategoryResponseDto>>(json);
            _output.WriteLine(json);
           
            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.OK);
            categories.ShouldNotBeEmpty();

        }
        [Fact]
        public async Task GetCategoryById_Ok_200()
        {
            // Act
            var result = await _httpClient.GetAsync($"api/categories/1");
            var json = await result.Content.ReadAsStringAsync();
            var category = SerializeHelper.Deserialize<AssetResponseDto>(json);

            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.OK);
            category.ShouldNotBeNull();
          

        }
        [Fact]
        public async Task GetCategoryById_NotFound_404()
        {
            // Act
            var result = await _httpClient.GetAsync($"api/categories/0");

            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);

        }
        [Fact]
        public async Task GetAssetsByCategoryId_Ok_200()
        {
            // Act
            var result = await _httpClient.GetAsync($"api/categories/1/assets");
            var json = await result.Content.ReadAsStringAsync();
            var categories = SerializeHelper.Deserialize<List<CategoryResponseDto>>(json);

            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.OK);
            categories.ShouldNotBeEmpty();
           
        }
        [Fact]
        public async Task GetAssetsByCategoryId_NotFound_404()
        {
            // Act
            var result = await _httpClient.GetAsync($"api/categories/4/assets");
            
            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);

        }
        [Fact]
        public async Task PostCategory_Ok_200()
        {
            // Arrange 
            var dto = new CreateCategoryCommand { Name = "Keyboard" };
            var json = JsonSerializer.Serialize(dto);

            // Act
            var result = await _httpClient.PostAsync($"api/categories", new StringContent(json, Encoding.UTF8, "application/json"));

            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.OK);

        }

        [Fact]
        public async Task PutCategoryById_Ok_200()
        {
            // Arrange
            var dto = new UpdateCategoryDto("Developer MAC");
            var json = SerializeHelper.Serialize(dto);
           
            // Act 
            var result = await _httpClient.PutAsync($"api/categories/1", new StringContent(json, Encoding.UTF8, "application/json"));
            var category = SerializeHelper.Deserialize<CategoryResponseDto>(await result.Content.ReadAsStringAsync());
            
            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.OK);
            category.Name.ShouldBe("Developer MAC");

        }
       
      
        
        [Fact]
        public async Task DeleteCategoryById_Ok_200()
        {
            
            // Act
            var result = await _httpClient.DeleteAsync($"api/categories/5");
            var check = await _httpClient.GetAsync($"api/categories/5");

            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.OK);
            check.StatusCode.ShouldBe(HttpStatusCode.NotFound);

        }
        [Fact]
        public async Task DeleteAssetById_BadRequest_400()
        {
            // Act
            var result = await _httpClient.DeleteAsync($"api/categories/1");
            
            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

    }
}
