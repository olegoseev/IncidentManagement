using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Notifications.Commands.Create;
using IoT.IncidentManagement.Application.Features.Notifications.Commands.Create.One;
using IoT.IncidentManagement.Application.UnitTests.Mocks;
using IoT.IncidentManagement.Domain.Enums;

using System;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Notifications.Create
{
    public class CreateNotificationHandlerTests : BaseTest
    {
        [Fact]
        public async Task Handle_AddNewNotificationToRepository()
        {
            var mockRepository = RepositoryMocks.GetNotificationRepository();

            var handler = new CreateNotificationHandler(mockRepository, _mapper);
            await handler.Handle(new CreateNotificationRequest
            {
                Type = NotificationType.UPDATE,
                State = NotificationState.WAITING,
                Order = 1,
                Interval = 10,
                IncidentId = 1,
                Repeat = true,
                InitTime = DateTime.UtcNow,
                Group = NotificationGroup.INTERNAL
            }, CancellationToken.None);

            var Notifications = await mockRepository.GetAllAsync();
            //Assert.Equal(3, Notifications.Count);
            Assert.Contains(Notifications, n => n.Type == NotificationType.UPDATE && n.State == NotificationState.WAITING);
        }

        [Fact]
        public async Task Handle_NullRequest()
        {
            var mockRepository = RepositoryMocks.GetNotificationRepository();
            var handler = new CreateNotificationHandler(mockRepository, _mapper);

            await Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public async Task Handle_RecordEmptyValidation()
        {
            var mockRepository = RepositoryMocks.GetNotificationRepository();
            var handler = new CreateNotificationHandler(mockRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new CreateNotificationRequest(), CancellationToken.None));
        }
    }
}
