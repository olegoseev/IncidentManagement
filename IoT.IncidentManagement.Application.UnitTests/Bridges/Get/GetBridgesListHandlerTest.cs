using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Bridges.Commands.Get.List;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Application.UnitTests.Mocks;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Bridges.Get
{
    public class GetBridgesListHandlerTest : BaseTest
    {

        [Fact]
        public async Task GetBridgesListTest()
        {
            var _mockBridgeRepository = RepositoryMocks.GetBridgeRepository();
            var handler = new GetBridgesListHandler(_mockBridgeRepository, _mapper);
            var result = await handler.Handle(new GetBridgesListRequest(), CancellationToken.None);

            Assert.IsType<List<BridgeDto>>(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task Handle_BadRequest()
        {
            var _mockBridgeRepository = RepositoryMocks.GetBridgeRepository();
            var handler = new GetBridgesListHandler(_mockBridgeRepository, _mapper);

            await Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }

    }
}
