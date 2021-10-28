using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Delete;
using IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Get.List;
using IoT.IncidentManagement.Application.UnitTests.Mocks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.ClosureActions.Delete
{
    public class DeleteActionHandlerTest : BaseTest
    {
        [Fact]
        public void Handle_BadRequest()
        {
            var _mockActionRepository = RepositoryMocks.GetClosureActionRepository();
            var handler = new DeleteClosureActionHandler(_mockActionRepository);

            Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public void Handle_NotFound()
        {
            var _mockActionRepository = RepositoryMocks.GetClosureActionRepository();
            var handler = new DeleteClosureActionHandler(_mockActionRepository);

            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(new DeleteClosureActionRequest { Id = 55 }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_DeleteActionFromRepository()
        {
            var _mockActionRepository = RepositoryMocks.GetClosureActionRepository();
            var handler = new DeleteClosureActionHandler(_mockActionRepository);
            await handler.Handle(new DeleteClosureActionRequest { Id = 1 }, CancellationToken.None);

            var getHandler = new GetClosureActionsListHandler(_mockActionRepository, _mapper);
            var result = await getHandler.Handle(new GetClosureActionsListRequest(), CancellationToken.None);

            Assert.Equal(5, result.Count);
        }
    }
}
