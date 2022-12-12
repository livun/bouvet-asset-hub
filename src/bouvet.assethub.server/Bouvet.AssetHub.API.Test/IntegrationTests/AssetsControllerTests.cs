using Bouvet.AssetHub.Contracts;
using Bouvet.AssetHub.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit.Abstractions;

namespace Bouvet.AssetHub.API.Tests
{
    public class AssetsControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly CustomWebApplicationFactory _factory;
        private readonly ITestOutputHelper _output;

        public AssetsControllerTests(CustomWebApplicationFactory factory, ITestOutputHelper output)
        {
            _factory = factory;
            _httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            _output = output;
        }
        [Fact]
        public async Task GetAssets_Ok_200()
        {
            // Act
            var result = await _httpClient.GetAsync($"api/assets");
            var json = await result.Content.ReadAsStringAsync();
            var assets = JsonSerializer.Deserialize<List<AssetResponseDto>>(json);
                       
            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.OK);
            assets.ShouldNotBeEmpty();
        }
        [Fact]
        public async Task PostAssets_Ok_200()
        {
            // Arrange 
            var dto = new CreateAssetDto ( "43215689", 1, Guid.NewGuid() );
            var json = JsonSerializer.Serialize(dto);
            
            // Act
            var result = await _httpClient.PostAsync($"api/assets", new StringContent(json, Encoding.UTF8, "application/json"));

            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
        [Fact]
        public async Task PostAssets_BadRequest_400()
        {
            // Arrange
            var dto = new CreateAssetDto ( "123456789", 0, Guid.NewGuid() );
            var json = JsonSerializer.Serialize(dto);
            
            // Act
            var result = await _httpClient.PostAsync($"api/assets", new StringContent(json, Encoding.UTF8, "application/json"));
            
            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task PutAssets_Ok_200()
        {
            // Arrange
            var dto = new UpdateAssetsByIdDto ( new List<int> { 1, 2 },Status.Available ) ;
            var json = SerializeHelper.Serialize(dto);
           
            // Act 
            var result = await _httpClient.PutAsync($"api/assets", new StringContent(json, Encoding.UTF8, "application/json"));
            
            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
        [Fact]
        public async Task GetAssetById_Ok_200()
        {
            // Act
            var result = await _httpClient.GetAsync($"api/assets/1");
            var json = await result.Content.ReadAsStringAsync();
            var asset = SerializeHelper.Deserialize<AssetResponseDto>(json);

            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.OK);
            asset.ShouldNotBeNull();
            asset.SerialNumberValue.ShouldBe("123456789");
            asset.Id.ShouldBe(1);
        }
        [Fact]
        public async Task GetAssetById_NotFound_404()
        {
            // Act
            var result = await _httpClient.GetAsync($"api/assets/0");

            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
        [Fact]
        public async Task PutAssetById_Ok_200()
        {
            // Arrange
            var dto = new UpdateAssetDto(Status.Discontinued, 2);
            var json = SerializeHelper.Serialize(dto);
            
            // Act
            var result = await _httpClient.PutAsync($"api/assets/1", new StringContent(json, Encoding.UTF8, "application/json"));
            var asset = SerializeHelper.Deserialize<AssetResponseDto>(await result.Content.ReadAsStringAsync());
            
            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.OK);
            asset.ShouldNotBeNull();
            asset.Status.ShouldBe(Status.Discontinued);
            asset.CategoryName.ShouldBe("Regular PC");
        }
        [Fact]
        public async Task PutAssetById_NotFound_404()
        {
            // Arrange
            var dto = new UpdateAssetDto(Status.Discontinued, 2);
            var json = SerializeHelper.Serialize(dto);
            
            // Act
            var result = await _httpClient.PutAsync($"api/assets/0", new StringContent(json, Encoding.UTF8, "application/json"));
            
            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
        [Fact]
        public async Task DeleteAssetById_Ok_200()
        {
            // Act
            var result = await _httpClient.DeleteAsync($"api/assets/2");
            var check = await _httpClient.GetAsync($"api/assets/2");

            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.OK);
            check.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
        [Fact]
        public async Task GetLoanByAssetId_Ok_200()
        {
            // Act
            var result = await _httpClient.GetAsync($"api/assets/5/loans");
            var json = await result.Content.ReadAsStringAsync();
            var assets = JsonSerializer.Deserialize<LoanResponseDto>(json);

            // Assert
            result.StatusCode.ShouldBe(HttpStatusCode.OK);
            assets.ShouldNotBeNull();
        }
    }
}
