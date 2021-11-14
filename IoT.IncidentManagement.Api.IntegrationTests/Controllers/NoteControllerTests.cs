using IoT.IncidentManagement.Api.IntegrationTests.Configuration;
using IoT.IncidentManagement.Application.Features.Notes.Commands.Create;
using IoT.IncidentManagement.Application.Features.Notes.Commands.Update;
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
    public class NoteControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        private static readonly string Uri = "/api/note";
        public NoteControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetDetailsReturnSuccessResult()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"{Uri}/1");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<NoteDto>(responseString);

            Assert.IsType<NoteDto>(result);
        }


        [Fact]
        public async Task GetNotesReturnSuccessResult()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"{Uri}/1/all");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<NoteDto>>(responseString);

            Assert.NotEmpty(result);
            Assert.IsType<List<NoteDto>>(result);
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
        public async Task UpdateReturnsNoContent(int id, string note)
        {
            var client = _factory.CreateClient();

            UpdateNoteRequest request = new() { Id = id, Record = note };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{Uri}", content);

            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);


            response = await client.GetAsync($"{Uri}/{id}");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var dto = JsonConvert.DeserializeObject<NoteDto>(responseString);

            Assert.Equal(note, dto.Record);

        }


        [Theory]
        [InlineData("Very Active")]
        public async Task CreateReturnsCreated(string record)
        {
            var client = _factory.CreateClient();

            CreateNoteRequest request = new() { IncidentId = 1, Record = record };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{Uri}", content);

            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);

            response = await client.GetAsync(response.Headers.Location.AbsolutePath);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var dto = JsonConvert.DeserializeObject<NoteDto>(responseString);

            Assert.Equal(record, dto.Record);
        }
    }
}
