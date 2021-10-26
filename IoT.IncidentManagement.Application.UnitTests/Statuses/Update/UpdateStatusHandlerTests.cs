using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Statuses.Commands.Update;
using IoT.IncidentManagement.Application.UnitTests.Mocks;
using IoT.IncidentManagement.Contstants;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Statuses.Update
{
    public class UpdateStatusHandlerTests : BaseTest
    {
        [Fact]
        public async Task Handle_UpdateStatusHandlerTest()
        {
            var mockRepository = RepositoryMocks.GetStatusRepository();
            var handler = new UpdateStatusHandler(mockRepository, _mapper);
            await handler.Handle(new UpdateStatusRequest
            {
                Id = 3,
                CurrentStatus = "Very good",
            }, CancellationToken.None);

            var status = await mockRepository.GetByIdAsync(3);

            Assert.Equal("Very good", status.CurrentStatus);
        }


        [Fact]
        public void Handle_BadRequest()
        {
            var mockRepository = RepositoryMocks.GetStatusRepository();
            var handler = new UpdateStatusHandler(mockRepository, _mapper);

            Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public void Handle_NotFound()
        {
            var mockRepository = RepositoryMocks.GetStatusRepository();
            var handler = new UpdateStatusHandler(mockRepository, _mapper);

            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(new UpdateStatusRequest { Id = 55 }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_RecordIsEmptyValidation()
        {
            var mockRepository = RepositoryMocks.GetStatusRepository();
            var handler = new UpdateStatusHandler(mockRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new UpdateStatusRequest { Id = 1 }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_RecordTooLongValidation()
        {
            var status = new string('A', ApplicationConstants.StatusMaxLen + 1);
            var mockRepository = RepositoryMocks.GetStatusRepository();
            var handler = new UpdateStatusHandler(mockRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new UpdateStatusRequest
            {
                Id = 1,
                CurrentStatus = status
            }, CancellationToken.None));
        }
    }
}
