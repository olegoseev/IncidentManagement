using IoT.IncidentManagement.Api.IntegrationTests.Configuration;
using IoT.IncidentManagement.Application.Features.Bridges.Commands.Create;
using IoT.IncidentManagement.Application.Features.Bridges.Commands.UpdateBridge;
using IoT.IncidentManagement.Application.Models;

using Newtonsoft.Json;

using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Api.IntegrationTests.Controllers
{
    public class BridgeControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        private readonly string Uri = "/api/bridge";

        public BridgeControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAllReturnSuccessResult()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"{Uri}/all");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<BridgeDto>>(responseString);

            Assert.NotEmpty(result);
            Assert.IsType<List<BridgeDto>>(result);
        }

        [Fact]
        public  async Task GetDetailsReturnSuccessResult()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"{Uri}/1");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<BridgeDto>(responseString);

            Assert.IsType<BridgeDto>(result);
        }

        [Fact]
        public async Task DeleteReturnsNoContent()
        {
            var client = _factory.CreateClient();
            var response = await client.DeleteAsync($"{Uri}/2");

            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);
        }

        [Theory]
        [InlineData(3, "NEW BRIDGE")]
        public async Task UpdateReturnsNoContent(int id, string bridgetype)
        {
            var client = _factory.CreateClient();

            UpdateBridgeRequest bridge = new() { Id = id, BridgeType = bridgetype };

            var content =  new StringContent(JsonConvert.SerializeObject(bridge), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{Uri}", content);

            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);


            response = await client.GetAsync($"{Uri}/{id}");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var dto = JsonConvert.DeserializeObject<BridgeDto>(responseString);

            Assert.Equal(bridgetype, dto.BridgeType);
        }


        [Fact]
        public async Task CreateReturnsCreated()
        {
            var client = _factory.CreateClient();

            CreateBridgeRequest bridge = new CreateBridgeRequest { BridgeType = "Test Bridge" };

            var content = new StringContent(JsonConvert.SerializeObject(bridge), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{Uri}", content);

            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);

            response = await client.GetAsync(response.Headers.Location.AbsolutePath);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var dto = JsonConvert.DeserializeObject<BridgeDto>(responseString);

            Assert.Equal("Test Bridge", dto.BridgeType);
        }
    }
}
