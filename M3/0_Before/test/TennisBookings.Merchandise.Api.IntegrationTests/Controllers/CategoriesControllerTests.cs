using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace TennisBookings.Merchandise.Api.IntegrationTests.Controllers
{
    public class CategoriesControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CategoriesControllerTests(WebApplicationFactory<Startup> factory)
        => _client = factory.CreateDefaultClient();

        [Fact]
        public async Task GetAll_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/api/categories");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetAll_ReturnsExpectedMediaType()
        {
            var response = await _client.GetAsync("/api/categories");

            Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
        }

        [Fact]
        public async Task GetAll_ReturnsContent()
        {
            var response = await _client.GetAsync("/api/categories");

            Assert.NotNull(response.Content);
            Assert.True(response.Content.Headers.ContentLength > 0);
        }

        [Fact]
        public async Task GetAll_ReturnedExpectedJson()
        {
            var responseStream = await _client.GetStreamAsync("/api/categories");

            var model = await JsonSerializer.DeserializeAsync<ExpectedCategoriesModel>(responseStream);

            Assert.Equal("{\"allowedCategories\":[\"Accessories\"," +
                          "\"Bags\",\"Balls\",\"Clothing\",\"Rackets\"]}", response);
        }
    }
}
