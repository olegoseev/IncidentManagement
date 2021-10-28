using IoT.IncidentManagement.Api.IntegrationTests.Configuration;
using IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Create;
using IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Update;
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
    public class ClosureActionControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        private static readonly string Uri = "/api/closureaction";

        public ClosureActionControllerTests(CustomWebApplicationFactory<Startup> factory)
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

            var result = JsonConvert.DeserializeObject<List<ClosureActionDto>>(responseString);

            Assert.NotEmpty(result);
            Assert.IsType<List<ClosureActionDto>>(result);
        }

        [Fact]
        public async Task GetDetailsReturnSuccessResult()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"{Uri}/1");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ClosureActionDto>(responseString);

            Assert.IsType<ClosureActionDto>(result);
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
        [InlineData(3, "NEW Action")]
        public async Task UpdateReturnsNoContent(int id, string action)
        {
            var client = _factory.CreateClient();

            UpdateClosureActionRequest request = new() { IncidentId = id, ToDoList = action };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{Uri}", content);

            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);


            response = await client.GetAsync($"{Uri}/{id}");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var dto = JsonConvert.DeserializeObject<ClosureActionDto>(responseString);

            Assert.Equal(action, dto.ToDoList);
        }


        [Fact]
        public async Task CreateReturnsCreated()
        {
            var client = _factory.CreateClient();

            CreateClosureActionRequest request = new() { IncidentId = 4, ToDoList = "Another new action" };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{Uri}", content);

            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);

            response = await client.GetAsync(response.Headers.Location.AbsolutePath);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var dto = JsonConvert.DeserializeObject<ClosureActionDto>(responseString);

            Assert.Equal("Another new action", dto.ToDoList);
        }
    }
}
