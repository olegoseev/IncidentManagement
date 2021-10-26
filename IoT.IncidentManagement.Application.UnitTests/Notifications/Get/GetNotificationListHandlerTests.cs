using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Notifications.Commands.Get.List;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Application.UnitTests.Mocks;
using IoT.IncidentManagement.Domain.Enums;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Notifications.Get
{
    public class GetNotificationListHandlerTests : BaseTest
    {
        [Fact]
        public async Task Handle_GetNotificationsListTest()
        {
            var mockRepository = RepositoryMocks.GetNotificationRepository();
            var handler = new GetNotificationListHandler(mockRepository, _mapper);
            var result = await handler.Handle(new GetNotificationListRequest
            {
                IncidentId = 2,
            }, CancellationToken.None);

            Assert.IsType<List<NotificationDto>>(result);
            Assert.Single(result.ToList());
        }

        [Fact]
        public void Handle_BadRequest()
        {
            var mockRepository = RepositoryMocks.GetNotificationRepository();
            var handler = new GetNotificationListHandler(mockRepository, _mapper);

            Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }
    }
}
