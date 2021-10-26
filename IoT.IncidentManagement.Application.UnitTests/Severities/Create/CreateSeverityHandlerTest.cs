using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Severities.Commands.Create;
using IoT.IncidentManagement.Application.UnitTests.Mocks;
using IoT.IncidentManagement.Contstants;

using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Severities.Create
{
    public class CreateSeverityHandlerTest : BaseTest
    {
        [Fact]
        public async Task Handle_AddNewSeverityToRepository()
        {
            var _mockSeverityRepository = RepositoryMocks.GetSeverityRepository();
            var handler = new CreateSeverityHandler(_mockSeverityRepository, _mapper);
            var result = await handler.Handle(new CreateSeverityRequest { IncidentSeverity = "P4", NotificationInterval = 240 }, CancellationToken.None);

            var severities = await _mockSeverityRepository.GetAllAsync();
            //Assert.Equal(4, severities.Count);
            Assert.Equal("P4", result.IncidentSeverity);
            Assert.Equal(240, result.NotificationInterval);
        }

        [Fact]
        public async Task Handle_SeverityAlreadyExistsValidation()
        {
            var _mockSeverityRepository = RepositoryMocks.GetSeverityRepository();
            var handler = new CreateSeverityHandler(_mockSeverityRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new CreateSeverityRequest 
            { 
                IncidentSeverity = "P1",
                NotificationInterval = 240 
            }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_BadRequest()
        {
            var _mockSeverityRepository = RepositoryMocks.GetSeverityRepository();
            var handler = new CreateSeverityHandler(_mockSeverityRepository, _mapper);

            await Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_SeverityEmptyValidation()
        {
            var _mockSeverityRepository = RepositoryMocks.GetSeverityRepository();
            var handler = new CreateSeverityHandler(_mockSeverityRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new CreateSeverityRequest { NotificationInterval = 100 }, CancellationToken.None));
        }


        [Fact]
        public async Task Handle_IntervalEmptyValidation()
        {
            var _mockSeverityRepository = RepositoryMocks.GetSeverityRepository();
            var handler = new CreateSeverityHandler(_mockSeverityRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new CreateSeverityRequest { IncidentSeverity = "NEW" }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_IntervalIsNegativeValidation()
        {
            var _mockSeverityRepository = RepositoryMocks.GetSeverityRepository();
            var handler = new CreateSeverityHandler(_mockSeverityRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(
                new CreateSeverityRequest { IncidentSeverity = "NEW", NotificationInterval = -1 }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_SeverityTooLongValidation()
        {
            var severity = new string('A', ApplicationConstants.SeverityMaxLen + 1);
            var _mockSeverityRepository = RepositoryMocks.GetSeverityRepository();
            var handler = new CreateSeverityHandler(_mockSeverityRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new CreateSeverityRequest
            {
                IncidentSeverity = severity,
                NotificationInterval = 100
            }, CancellationToken.None));
        }
    }
}
