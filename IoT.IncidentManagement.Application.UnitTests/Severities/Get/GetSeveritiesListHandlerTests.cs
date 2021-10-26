using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Severities.Commands.Get.List;
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
    public class GetSeveritiesListHandlerTests : BaseTest
    {
        [Fact]
        public async Task GetSeveritiesList()
        {
            var _mockSeverityRepository = RepositoryMocks.GetSeverityRepository();

            var getHandler = new GetSeveritiesListHandler(_mockSeverityRepository, _mapper);
            var result = await getHandler.Handle(new GetSeveritiesListRequest(), CancellationToken.None);

            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task Handle_BadRequest()
        {
            var _mockSeverityRepository = RepositoryMocks.GetSeverityRepository();
            var handler = new GetSeveritiesListHandler(_mockSeverityRepository, _mapper);

            await Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }
    }
}
