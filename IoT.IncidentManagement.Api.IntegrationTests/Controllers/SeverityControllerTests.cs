using IoT.IncidentManagement.Api.IntegrationTests.Configuration;
using IoT.IncidentManagement.Application.Features.Severities.Commands.Create;
using IoT.IncidentManagement.Application.Features.Severities.Commands.Update;
using IoT.IncidentManagement.Application.Models;

using Microsoft.VisualStudio.TestPlatform.TestHost;

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
    public class SeverityControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        private static string Uri = "/api/severity";
        public SeverityControllerTests(CustomWebApplicationFactory<Program> factory)
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

            var result = JsonConvert.DeserializeObject<List<SeverityDto>>(responseString);

            Assert.NotEmpty(result);
            Assert.IsType<List<SeverityDto>>(result);
        }

        [Fact]
        public async Task GetDetailsReturnSuccessResult()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"{Uri}/1");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<SeverityDto>(responseString);

            Assert.IsType<SeverityDto>(result);
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
        [InlineData(3, "P4", 360)]
        public async Task UpdateReturnsNoContent(int id, string severity, int interval)
        {
            var client = _factory.CreateClient();

            UpdateSeverityRequest request = new UpdateSeverityRequest { Id = id, IncidentSeverity = severity, NotificationInterval = interval };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{Uri}", content);

            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);


            response = await client.GetAsync($"{Uri}/{id}");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var dto = JsonConvert.DeserializeObject<SeverityDto>(responseString);

            Assert.Equal(severity, dto.IncidentSeverity);
            Assert.Equal(interval, dto.NotificationInterval);
        }


        [Theory]
        [InlineData("P0", 15)]
        public async Task CreateReturnsCreated(string severity, int interval)
        {
            var client = _factory.CreateClient();

            CreateSeverityRequest request = new CreateSeverityRequest { IncidentSeverity = severity, NotificationInterval = interval };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{Uri}", content);

            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);

            response = await client.GetAsync(response.Headers.Location.AbsolutePath);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var dto = JsonConvert.DeserializeObject<SeverityDto>(responseString);

            Assert.Equal(severity, dto.IncidentSeverity);
            Assert.Equal(interval, dto.NotificationInterval);
        }
    }
}
