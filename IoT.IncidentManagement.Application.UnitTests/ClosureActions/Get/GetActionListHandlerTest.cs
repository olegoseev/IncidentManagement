using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Get.List;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Application.UnitTests.Mocks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.ClosureActions.Get
{
    public class GetActionListHandlerTest : BaseTest
    {
        [Fact]
        public async Task Handle_GetActionsListTest()
        {
            var _mockActionRepository = RepositoryMocks.GetClosureActionRepository();
            var handler = new GetActionsListHandler(_mockActionRepository, _mapper);
            var result = await handler.Handle(new GetClosureActionsListRequest(), CancellationToken.None);

            Assert.IsType<List<ClosureActionDto>>(result);
            Assert.Equal(6, result.Count);
        }

        [Fact]
        public void Handle_BadRequest()
        {
            var _mockActionRepository = RepositoryMocks.GetClosureActionRepository();
            var handler = new GetActionsListHandler(_mockActionRepository, _mapper);

            Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }
    }
}
