using IoT.IncidentManagement.Application.Features.Incidents.Commands.Update;
using IoT.IncidentManagement.Application.UnitTests.Mocks;
using IoT.IncidentManagement.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Incidents.Update
{
    public class UpdateIncidentHandlerTests : BaseTest
    {
        [Fact]
        public async Task Handle_AddNewActionToRepository()
        {
            var _mockActionRepository = RepositoryMocks.GetIncidentRepository();
            var handler = new UpdateIncidentHandler(_mockActionRepository, _mapper);
            await handler.Handle(new UpdateIncidentRequest
            {
                Id = 1,
                IncidentCase = "INC000000022",
                BridgeId = 1,
                Description = "New case",
                CustomerImpact = "Not so much",
                SeverityId = 2,
                NotifiedTime = DateTime.Now,
                StartTime = DateTime.Now,
                StatusId = 1
            }, CancellationToken.None); ;

            var fromDb = await _mockActionRepository.GetByIdAsync(1);
            Assert.IsType<Incident>(fromDb);
            Assert.Equal("New case", fromDb.Description);
            Assert.Equal("Not so much", fromDb.CustomerImpact);
            Assert.Equal("INC000000022", fromDb.IncidentCase);
        }
    }
}
