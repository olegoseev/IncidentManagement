using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Severities.Commands.Update;
using IoT.IncidentManagement.Application.UnitTests.Mocks;
using IoT.IncidentManagement.Contstants;

using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Severities.Update
{
    public class UpdateSeverityHandlerTest : BaseTest
    {
        [Theory]
        [InlineData("NEW", 240)]
        public async Task Handle_UpdateSeverityHandlerTest(string severity, int interval)
        {
            var _mockSeverityRepository = RepositoryMocks.GetSeverityRepository();
            var handler = new UpdateSeverityHandler(_mockSeverityRepository, _mapper);
            await handler.Handle(new UpdateSeverityRequest { Id = 1, IncidentSeverity = severity, NotificationInterval = interval }, CancellationToken.None);


            var result = await _mockSeverityRepository.GetByIdAsync(1);

            Assert.Equal(severity, result.IncidentSeverity);
            Assert.Equal(interval, result.NotificationInterval);
        }


        [Fact]
        public async Task Handle_ReguestIsNull()
        {
            var _mockSeverityRepository = RepositoryMocks.GetSeverityRepository();
            var handler = new UpdateSeverityHandler(_mockSeverityRepository, _mapper);

            await Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public async Task Handle_NotFound()
        {
            var _mockSeverityRepository = RepositoryMocks.GetSeverityRepository();
            var handler = new UpdateSeverityHandler(_mockSeverityRepository, _mapper);

            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(new UpdateSeverityRequest { Id = 55 }, CancellationToken.None));
        }


        [Fact]
        public async Task Handle_SeverityEmptyValidation()
        {
            var _mockSeverityRepository = RepositoryMocks.GetSeverityRepository();
            var udpateHandler = new UpdateSeverityHandler(_mockSeverityRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => udpateHandler.Handle(new UpdateSeverityRequest { Id = 1, NotificationInterval = 100 }, CancellationToken.None));
        }


        [Fact]
        public async Task Handle_IntervalEmptyValidation()
        {
            var _mockSeverityRepository = RepositoryMocks.GetSeverityRepository();
            var udpateHandler = new UpdateSeverityHandler(_mockSeverityRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => udpateHandler.Handle(new UpdateSeverityRequest { Id = 1, IncidentSeverity = "NEW" }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_IntervalIsNegativeValidation()
        {
            var _mockSeverityRepository = RepositoryMocks.GetSeverityRepository();
            var udpateHandler = new UpdateSeverityHandler(_mockSeverityRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => udpateHandler.Handle(new UpdateSeverityRequest { Id = 1, IncidentSeverity = "NEW", NotificationInterval = -1 }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_SeverityTooLongValidation()
        {
            var severity = new string('A', ApplicationConstants.SeverityMaxLen + 1);
            var _mockSeverityRepository = RepositoryMocks.GetSeverityRepository();
            var udpateHandler = new UpdateSeverityHandler(_mockSeverityRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => udpateHandler.Handle(new UpdateSeverityRequest
            {
                Id = 1,
                IncidentSeverity = severity,
                NotificationInterval = 100
            }, CancellationToken.None));
        }
    }
}
