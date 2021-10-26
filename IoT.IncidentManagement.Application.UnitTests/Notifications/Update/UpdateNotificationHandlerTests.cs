using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Notifications.Commands.Update;
using IoT.IncidentManagement.Application.UnitTests.Mocks;
using IoT.IncidentManagement.Domain.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Notifications.Update
{
    public class UpdateNotificationHandlerTests : BaseTest
    {
        [Fact]
        public async Task Handle_UpdateNotificationHandlerTest()
        {
            var mockRepository = RepositoryMocks.GetNotificationRepository();
            var handler = new UpdateNotificationHandler(mockRepository, _mapper);
            await handler.Handle(new UpdateNotificationRequest
            {
                Id = 1,
                Type = NotificationType.INITIAL,
                State = NotificationState.OFF,
                Order = 1,
                Interval = 10,
                IncidentId = 1,
                Repeat = false,
                InitTime = DateTime.UtcNow,
                Group = NotificationGroup.INTERNAL


            }, CancellationToken.None);

            var notification = await mockRepository.GetByIdAsync(1);

            Assert.Equal(NotificationState.OFF, notification.State);
        }


        [Fact]
        public void Handle_BadRequest()
        {
            var mockRepository = RepositoryMocks.GetNotificationRepository();
            var handler = new UpdateNotificationHandler(mockRepository, _mapper);

            Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public void Handle_NotFound()
        {
            var mockRepository = RepositoryMocks.GetNotificationRepository();
            var handler = new UpdateNotificationHandler(mockRepository, _mapper);

            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(new UpdateNotificationRequest { Id = 55 }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_RecordIsEmptyValidation()
        {
            var mockRepository = RepositoryMocks.GetNotificationRepository();
            var handler = new UpdateNotificationHandler(mockRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new UpdateNotificationRequest { Id = 1 }, CancellationToken.None));
        }

    }
}
