using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Bridges.Commands.Get.Details;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Application.UnitTests.Mocks;

using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Bridges.Get
{
    public class GetBridgeDetailsHandlerTest : BaseTest
    {

        [Theory]
        [InlineData(1, "IoT Triage")]
        [InlineData(2, "CMD")]
        [InlineData(3, "NOC")]
        public async Task Handle_GetBridgeDetails(int Id, string bridgeType)
        {
            var _mockBridgeRepository = RepositoryMocks.GetBridgeRepository();
            var handler = new GetBridgeDetailsHandler(_mockBridgeRepository, _mapper);

            var result = await handler.Handle(new GetBridgeDetailsRequest { Id = Id }, CancellationToken.None);
            Assert.IsType<BridgeDto>(result);
            Assert.Equal(bridgeType, result.BridgeType);
        }


        [Fact]
        public void Handle_BadRequest()
        {
            var _mockBridgeRepository = RepositoryMocks.GetBridgeRepository();
            var handler = new GetBridgeDetailsHandler(_mockBridgeRepository, _mapper);

            Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public void Handle_NotFound()
        {
            var _mockBridgeRepository = RepositoryMocks.GetBridgeRepository();
            var handler = new GetBridgeDetailsHandler(_mockBridgeRepository, _mapper);

            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(new GetBridgeDetailsRequest { Id = 55 }, CancellationToken.None));
        }
    }
}
