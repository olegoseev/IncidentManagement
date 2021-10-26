using IoT.IncidentManagement.Api.IntegrationTests.Configuration;
using IoT.IncidentManagement.Application.Features.ManagerActions.Commands.Create.Group;
using IoT.IncidentManagement.Application.Features.ManagerActions.Commands.Create.One;
using IoT.IncidentManagement.Application.Features.ManagerActions.Commands.Update;
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
    public class ManagerActionControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        private readonly string Uri = "/api/ManagerAction";

        public ManagerActionControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Should_create_manager_action()
        {
            var client = _factory.CreateClient();

            CreateManagerActionRequest request = new() {
                IncidentId = 4,  
                Description = "Action for 4",
                InitTime = DateTime.UtcNow,
                Interval = 30,
                Order = 1,
                Repeat = false,
                State = Domain.Enums.NotificationState.WAITING,
             };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{Uri}", content);

            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);

            response = await client.GetAsync(response.Headers.Location.AbsolutePath);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var dto = JsonConvert.DeserializeObject<ManagerActionDto>(responseString);

            Assert.Equal("Action for 4", dto.Description);
        }

        [Fact]
        public async Task Should_create_manager_action_group()
        {
            var client = _factory.CreateClient();

            CreateManagerActionGroupRequest request = new()
            {
                IncidentId = 3
            };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{Uri}/group", content);

            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);

            response = await client.GetAsync(response.Headers.Location.AbsolutePath);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var dto = JsonConvert.DeserializeObject<List<ManagerActionDto>>(responseString);

            Assert.NotEmpty(dto);
        }

        [Fact]
        public async Task Delete_returns_no_content()
        {
            var client = _factory.CreateClient();
            var response = await client.DeleteAsync($"{Uri}/10");

            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);
        }


        [Fact]
        public async Task Update_should_return_no_content()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync($"{Uri}/10");

            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<ManagerActionDto>(responseString);

            UpdateManagerActionRequest request = new UpdateManagerActionRequest
            {
                Id = dto.Id,
                Description = dto.Description,
                IncidentId = dto.IncidentId,
                InitTime = dto.InitTime,
                Interval = dto.Interval,
                Order = dto.Order,
                Repeat = true,
                State = dto.State,
            };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            response = await client.PutAsync($"{Uri}", content);
            response.EnsureSuccessStatusCode();
        }


        [Fact]
        public async Task Get_action_details_should_return_ok()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync($"{Uri}/9");

            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<ManagerActionDto>(responseString);
            Assert.Equal("Action9", dto.Description);
        }
    }
}
