using IoT.IncidentManagement.Api.IntegrationTests.Configuration;
using IoT.IncidentManagement.Application.Features.Incidents.Commands.Create;
using IoT.IncidentManagement.Application.Models;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Api.IntegrationTests.Controllers
{
    public class IncidentControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        private readonly string Uri = "/api/incident";

        public IncidentControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateReturnsCreated()
        {
            var client = _factory.CreateClient();

        CreateIncidentRequest incident = new()
            {
                IncidentCase = "INC0abdcdesbd",
                Description = "test incident controller",
                CustomerImpact = "I hope it will pass the test",
                StartTime = DateTime.UtcNow,
                NotifiedTime = DateTime.UtcNow,
                BridgeId = 1,
                SeverityId = 1,
                StatusId = 1,
            };

            var content = new StringContent(JsonConvert.SerializeObject(incident), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{Uri}", content);

            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);

            response = await client.GetAsync(response.Headers.Location.AbsolutePath);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var dto = JsonConvert.DeserializeObject<IncidentDto>(responseString);

            Assert.Equal("INC0abdcdesbd", dto.IncidentCase);
        }

        [Fact]
        public async Task GetIncidentsReturnsSuccessResult()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"{Uri}");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<IncidentDto>> (responseString);

            Assert.IsAssignableFrom<IEnumerable<IncidentDto>>(result);
        }
    }
}
