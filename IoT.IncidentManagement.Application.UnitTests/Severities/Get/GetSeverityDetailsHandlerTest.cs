using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Severities.Commands.Get.Details;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Application.UnitTests.Mocks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Severities.Get
{
    public class GetSeverityDetailsHandlerTest : BaseTest
    {
        [Theory]
        [InlineData(1, "P1", 60)]
        [InlineData(2, "P2", 90)]
        [InlineData(3, "P3", 120)]
        public async Task Handle_GetSeverityDetails(int Id, string severity, int interval)
        {
            var _mockSeverityRepository = RepositoryMocks.GetSeverityRepository();
            var handler = new GetSeverityDetailsHandler(_mockSeverityRepository, _mapper);

            var result = await handler.Handle(new GetSeverityDetailsRequest { Id = Id }, CancellationToken.None);
            Assert.IsType<SeverityDto>(result);
            Assert.Equal(severity, result.IncidentSeverity);
            Assert.Equal(interval, result.NotificationInterval);
        }


        [Fact]
        public void Handle_BadRequest()
        {
            var _mockSeverityRepository = RepositoryMocks.GetSeverityRepository();
            var handler = new GetSeverityDetailsHandler(_mockSeverityRepository, _mapper);

            Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public void Handle_NotFound()
        {
            var _mockSeverityRepository = RepositoryMocks.GetSeverityRepository();
            var handler = new GetSeverityDetailsHandler(_mockSeverityRepository, _mapper);

            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(new GetSeverityDetailsRequest { Id = 55 }, CancellationToken.None));
        }
    }
}
