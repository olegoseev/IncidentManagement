using IoT.IncidentManagement.Api.IntegrationTests.Configuration;
using IoT.IncidentManagement.Application.Features.Statuses.Commands.Create;
using IoT.IncidentManagement.Application.Features.Statuses.Commands.Update;
using IoT.IncidentManagement.Application.Models;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Api.IntegrationTests.Controllers
{
    public class StatusControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        private static string Uri = "/api/status";
        public StatusControllerTests(CustomWebApplicationFactory<Startup> factory)
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

            var result = JsonConvert.DeserializeObject<List<StatusDto>>(responseString);

            Assert.NotEmpty(result);
            Assert.IsType<List<StatusDto>>(result);
        }

        [Fact]
        public async Task GetDetailsReturnSuccessResult()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"{Uri}/1");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<StatusDto>(responseString);

            Assert.IsType<StatusDto>(result);
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
        [InlineData(3, "Active")]
        public async Task UpdateReturnsNoContent(int id, string status)
        {
            var client = _factory.CreateClient();

            UpdateStatusRequest request = new UpdateStatusRequest { Id = id, CurrentStatus =  status};

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{Uri}", content);

            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);


            response = await client.GetAsync($"{Uri}/{id}");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var dto = JsonConvert.DeserializeObject<StatusDto>(responseString);

            Assert.Equal(status, dto.CurrentStatus);

        }


        [Theory]
        [InlineData("Very Active")]
        public async Task CreateReturnsCreated(string status)
        {
            var client = _factory.CreateClient();

            CreateStatusRequest request = new CreateStatusRequest { CurrentStatus = status };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{Uri}", content);

            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);

            response = await client.GetAsync(response.Headers.Location.AbsolutePath);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var dto = JsonConvert.DeserializeObject<StatusDto>(responseString);

            Assert.Equal(status, dto.CurrentStatus);
        }
    }
}
