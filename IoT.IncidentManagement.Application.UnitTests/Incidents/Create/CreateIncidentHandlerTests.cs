using IoT.IncidentManagement.Application.Features.Incidents.Commands.Create;
using IoT.IncidentManagement.Application.UnitTests.Mocks;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using Moq;

using System;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Incidents.Create
{
    public class CreateIncidentHandlerTests : BaseTest
    {

        [Fact]
        public async Task Handle_AddNewActionToRepository()
        {
            var _mediator = new Mock<IMediator>().Object;
            var _mockActionRepository = RepositoryMocks.GetIncidentRepository();
            var handler = new CreateIncidentHandler(_mockActionRepository, _mapper);
            var incident = await handler.Handle(new CreateIncidentRequest
            {
                IncidentCase = "INC000000022",
                BridgeId = 1,
                Description = "New case",
                CustomerImpact = "Not so much",
                SeverityId = 2,
                NotifiedTime = DateTime.Now,
                StartTime = DateTime.Now,
                StatusId = 1
            }, CancellationToken.None); ;

            var fromDb = await _mockActionRepository.GetByIdAsync(incident.Id);
            Assert.IsType<Incident>(fromDb);
            Assert.Equal(incident.Description, fromDb.Description);
            Assert.Equal(incident.CustomerImpact, fromDb.CustomerImpact);
            Assert.Equal(incident.IncidentCase, fromDb.IncidentCase);
        }
    }
}

