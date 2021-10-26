using IoT.IncidentManagement.Api.IntegrationTests.Configuration;
using IoT.IncidentManagement.Application.Features.Notifications.Commands.Create.Group;
using IoT.IncidentManagement.Application.Features.Notifications.Commands.Create.One;
using IoT.IncidentManagement.Application.Features.Notifications.Commands.Update;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Enums;

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
    public class NotificationControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;

        private readonly string Uri = "/api/Notification";

        public NotificationControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }


        [Fact]
        public async Task GetIncidentNotifications_should_return_list()
        {
            var client = factory.CreateClient();
            var response = await client.GetAsync($"{Uri}/1/all");

            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<List<NotificationDto>>(responseString);
            Assert.IsType<List<NotificationDto>>(dto);
            Assert.NotEmpty(dto);
        }

        [Fact]
        public async Task GetIncidentNotificationTypes_should_return_ok()
        {
            var client = factory.CreateClient();
            var response = await client.GetAsync($"{Uri}/1/types");

            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<IncidentNotificationGroup>(responseString);
            Assert.True(dto.InternalNotificationEnabled);
            Assert.False(dto.ExternalNotificationEnabled);
        }

        [Fact]
        public async Task DeleteGroup_should_return_no_content()
        {
            var client = factory.CreateClient();
            var response = await client.DeleteAsync($"{Uri}/2/group/INTERNAL");

            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);
        }


        [Fact]
        public async Task CreateNewNotification_should_return_ok()
        {
            var client = factory.CreateClient();

            CreateNotificationRequest request = new CreateNotificationRequest
            {
                IncidentId = 1,
                InitTime = DateTime.UtcNow,
                Interval = 15,
                Group = NotificationGroup.INTERNAL,
                Type = NotificationType.INITIAL,
                Order = 1,
                Repeat = false,
                State = NotificationState.WAITING,

            };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{Uri}", content);

            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);

            var responseString = await response.Content.ReadAsStringAsync();

            var dto = JsonConvert.DeserializeObject<NotificationDto>(responseString);

           Assert.NotEqual(0, dto.Id);
        }

        [Fact]
        public async Task CreateNewNotificationGroup_should_return_ok()
        {
            var client = factory.CreateClient();

            CreateNotificationGroupRequest request = new CreateNotificationGroupRequest
            {
                IncidentId = 1,
                Group = NotificationGroup.INTERNAL,
                Interval = 15,
            };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{Uri}/group", content);

            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task UpdateNotification_should_return_no_content()
        {
            var client = factory.CreateClient();

            UpdateNotificationRequest request = new UpdateNotificationRequest
            {
                IncidentId = 1,
                Id = 2,
                Order = 2,
                InitTime = DateTime.UtcNow,
                Interval = 15,
                Repeat = false,
                State = NotificationState.WAITING,
                Group = NotificationGroup.INTERNAL,
                Type = NotificationType.UPDATE,
            };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{Uri}", content);

            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);
        }

    }
}
