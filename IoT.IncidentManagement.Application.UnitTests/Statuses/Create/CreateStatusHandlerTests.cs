using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Statuses.Commands.Create;
using IoT.IncidentManagement.Application.UnitTests.Mocks;
using IoT.IncidentManagement.Contstants;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Statuses.Create
{
    public class CreateStatusHandlerTests : BaseTest
    {
        [Fact]
        public async Task Handle_AddNewStatusToRepository()
        {
            var mockRepository = RepositoryMocks.GetStatusRepository();

            var handler = new CreateStatusHandler(mockRepository, _mapper);
            await handler.Handle(new CreateStatusRequest { CurrentStatus = "Very Bad" }, CancellationToken.None);

            var status = (await mockRepository.GetAllAsync()).ToList();
            Assert.Equal(4, status.Count);
            Assert.Contains(status, b => b.CurrentStatus == "Very Bad");
        }

        [Fact]
        public async Task Handle_NullRequest()
        {
            var mockRepository = RepositoryMocks.GetStatusRepository();
            var incidentRepository = RepositoryMocks.GetIncidentRepository();
            var handler = new CreateStatusHandler(mockRepository, _mapper);

            await Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public async Task Handle_RecordEmptyValidation()
        {
            var mockRepository = RepositoryMocks.GetStatusRepository();
            var incidentRepository = RepositoryMocks.GetIncidentRepository();
            var handler = new CreateStatusHandler(mockRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new CreateStatusRequest(), CancellationToken.None));
        }


        [Fact]
        public async Task Handle_StatusTooLongValidation()
        {
            var record = new string('A', ApplicationConstants.StatusMaxLen + 1);

            var mockRepository = RepositoryMocks.GetStatusRepository();
            var incidentRepository = RepositoryMocks.GetIncidentRepository();
            var handler = new CreateStatusHandler(mockRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new CreateStatusRequest
            {
                CurrentStatus = record
            }, CancellationToken.None));
        }
    }
}
