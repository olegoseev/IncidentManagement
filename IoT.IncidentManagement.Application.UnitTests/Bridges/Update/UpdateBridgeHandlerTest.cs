using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Bridges.Commands.Get.Details;
using IoT.IncidentManagement.Application.Features.Bridges.Commands.UpdateBridge;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Application.UnitTests.Mocks;
using IoT.IncidentManagement.Contstants;

using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Bridges.Update
{
    public class UpdateBridgeHandlerTest : BaseTest
    {
        [Fact]
        public async Task Handle_UpdateBridgeHandlerTest()
        {
            var _mockBridgeRepository = RepositoryMocks.GetBridgeRepository();
            var udpateHandler = new UpdateBridgeHandler(_mockBridgeRepository, _mapper);
            await udpateHandler.Handle(new UpdateBridgeRequest { Id = 1, BridgeType = "NEW" }, CancellationToken.None);


            var getHandler = new GetBridgeDetailsHandler(_mockBridgeRepository, _mapper);

            var result = await getHandler.Handle(new GetBridgeDetailsRequest { Id = 1 }, CancellationToken.None);
            Assert.IsType<BridgeDto>(result);
            Assert.Equal("NEW", result.BridgeType);
        }


        [Fact]
        public async Task Handle_RequestIsNull()
        {
            var _mockBridgeRepository = RepositoryMocks.GetBridgeRepository();
            var handler = new UpdateBridgeHandler(_mockBridgeRepository, _mapper);

            await Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public async Task Handle_NotFound()
        {
            var _mockBridgeRepository = RepositoryMocks.GetBridgeRepository();
            var handler = new UpdateBridgeHandler(_mockBridgeRepository, _mapper);

            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(new UpdateBridgeRequest { Id = 55 }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_BridgeTypeEmptyValidation()
        {
            var _mockBridgeRepository = RepositoryMocks.GetBridgeRepository();
            var handler = new UpdateBridgeHandler(_mockBridgeRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new UpdateBridgeRequest { Id = 1}, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_BridgeTypeTooLongValidation()
        {
            var bridge = new string('A', ApplicationConstants.BridgeTypeMaxLen + 1);
            var _mockBridgeRepository = RepositoryMocks.GetBridgeRepository();
            var handler = new UpdateBridgeHandler(_mockBridgeRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new UpdateBridgeRequest
            {
                Id = 1,
                BridgeType = bridge
            }, CancellationToken.None));
        }
    }
}
