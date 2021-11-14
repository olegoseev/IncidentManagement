using IoT.IncidentManagement.Api.IntegrationTests.Configuration;
using IoT.IncidentManagement.Application.Features.Participants.Commands.Create;
using IoT.IncidentManagement.Application.Features.Participants.Commands.Update;
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
    public class ParticipantControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        private static string Uri = "/api/participant";
        public ParticipantControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetParticipantForIncidentReturnSuccessResult()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"{Uri}/1");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ParticipantDto>(responseString);

            Assert.IsType<ParticipantDto>(result);
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
        [InlineData(3, "Someone from IT")]
        public async Task UpdateReturnsNoContent(int id, string participant)
        {
            var client = _factory.CreateClient();

            UpdateParticipantRequest request = new UpdateParticipantRequest { IncidentId = id, Group = participant };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{Uri}", content);

            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);


            response = await client.GetAsync($"{Uri}/{id}");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var dto = JsonConvert.DeserializeObject<ParticipantDto>(responseString);

            Assert.Equal(participant, dto.Group);

        }


        [Theory]
        [InlineData("Very Active")]
        public async Task CreateReturnsCreated(string participant)
        {

            await DeleteReturnsNoContent();

            var client = _factory.CreateClient();

            CreateParticipantRequest request = new CreateParticipantRequest { IncidentId = 2, Group = participant };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{Uri}", content);

            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);

            response = await client.GetAsync(response.Headers.Location.AbsolutePath);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var dto = JsonConvert.DeserializeObject<ParticipantDto>(responseString);

            Assert.Equal(participant, dto.Group);
        }

    }
}
