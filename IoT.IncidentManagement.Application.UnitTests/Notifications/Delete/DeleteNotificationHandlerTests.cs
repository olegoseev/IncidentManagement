using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Notifications.Commands.Delete;
using IoT.IncidentManagement.Application.Features.Notifications.Commands.Delete.One;
using IoT.IncidentManagement.Application.UnitTests.Mocks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Notifications.Delete
{
    public class DeleteNotificationHandlerTests : BaseTest
    {
        [Fact]
        public void Handle_BadRequest()
        {
            var mockRepository = RepositoryMocks.GetNotificationRepository();
            var handler = new DeleteNotificationHandler(mockRepository);

            Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public void Handle_NotFound()
        {
            var mockRepository = RepositoryMocks.GetNotificationRepository();
            var handler = new DeleteNotificationHandler(mockRepository);

            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(new DeleteNotificationRequest { Id = 55 }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_DeleteNotificationFromRepository()
        {
            var mockRepository = RepositoryMocks.GetNotificationRepository();
            var handler = new DeleteNotificationHandler(mockRepository);
            await handler.Handle(new DeleteNotificationRequest { Id = 1 }, CancellationToken.None);

            var result = (await mockRepository.GetAllAsync()).ToList();

            Assert.Single(result);
        }
    }
}
