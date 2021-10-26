using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Incidents.Commands.Get.DetailsExtended;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Application.UnitTests.Mocks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Incidents.Get
{
    public class GetIncidentExtDetailsHandlerTests : BaseTest
    {
        [Theory]
        [InlineData(1, "INC00123456")]
        [InlineData(2, "INC00654321")]
        public async Task GetActionDetailsTest(int Id, string Incident)
        {
            var mockRepository = RepositoryMocks.GetIncidentRepository();
            var handler = new GetIncidentDetailsExtHandler(mockRepository, _mapper);

            var result = await handler.Handle(new GetIncidentDetailsExtRequest { Id = Id }, CancellationToken.None);
            var fromData = await mockRepository.GetByIdAsync(Id);
            Assert.IsType<IncidentExtDto>(result);
            Assert.Equal(Incident, result.IncidentCase);
            Assert.Equal(fromData.Bridge.BridgeType, result.Bridge.BridgeType);
            Assert.Equal(fromData.Status.CurrentStatus, result.Status.CurrentStatus);
            Assert.Equal(fromData.Severity.IncidentSeverity, result.Severity.IncidentSeverity);
            Assert.Equal(fromData.Notes.Count, result.Notes.Count);
            Assert.Equal(fromData.Participant.Group, result.Participant);
        }


        [Fact]
        public void Handle_BadRequest()
        {
            var mockRepository = RepositoryMocks.GetIncidentRepository();
            var handler = new GetIncidentDetailsExtHandler(mockRepository, _mapper);

            Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public void Handle_NotFound()
        {
            var mockRepository = RepositoryMocks.GetIncidentRepository();
            var handler = new GetIncidentDetailsExtHandler(mockRepository, _mapper);

            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(new GetIncidentDetailsExtRequest { Id = 55 }, CancellationToken.None));
        }
    }
}
