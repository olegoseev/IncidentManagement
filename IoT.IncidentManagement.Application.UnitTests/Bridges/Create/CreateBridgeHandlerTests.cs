using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Bridges.Commands.Create;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Application.UnitTests.Mocks;
using IoT.IncidentManagement.Contstants;

using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Bridges.Create
{
    public class CreateBridgeHandlerTests : BaseTest
    {

        [Fact]
        public async Task Handle_AddNewBridgeToRepository()
        {
            var _mockBridgeRepository = RepositoryMocks.GetBridgeRepository();

            var handler = new CreateBridgeHandler(_mockBridgeRepository, _mapper);
            var result = await handler.Handle(new CreateBridgeRequest { BridgeType = "OPS" }, CancellationToken.None);

            var bridges = await _mockBridgeRepository.GetAllAsync();
            Assert.NotNull(result);
            Assert.IsType<BridgeDto>(result);
            //Assert.Equal(4, bridges.Count);
            Assert.Contains(bridges, b => b.BridgeType == "OPS");
        }



        [Fact]
        public async Task Handle_BadRequest()
        {
            var _mockBridgeRepository = RepositoryMocks.GetBridgeRepository();
            var handler = new CreateBridgeHandler(_mockBridgeRepository, _mapper);

            await Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_BridgeAlreadyExistsValidation()
        {
            var _mockBridgeRepository = RepositoryMocks.GetBridgeRepository();
            var handler = new CreateBridgeHandler(_mockBridgeRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new CreateBridgeRequest { BridgeType = "CMD" }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_BridgeTypeEmptyValidation()
        {
            var _mockBridgeRepository = RepositoryMocks.GetBridgeRepository();
            var handler = new CreateBridgeHandler(_mockBridgeRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new CreateBridgeRequest(), CancellationToken.None));
        }


        [Fact]
        public async Task Handle_BridgeTooLongValidation()
        {
            var bridge = new string('A', ApplicationConstants.BridgeTypeMaxLen + 1);
            var _mockBridgeRepository = RepositoryMocks.GetBridgeRepository();
            var handler = new CreateBridgeHandler(_mockBridgeRepository, _mapper);
           await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new CreateBridgeRequest
            {
                BridgeType = bridge
            }, CancellationToken.None));
        }
    }
}
