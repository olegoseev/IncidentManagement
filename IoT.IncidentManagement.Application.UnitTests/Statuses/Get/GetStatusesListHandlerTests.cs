using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Statuses.Commands.Get.List;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Application.UnitTests.Mocks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Statuses.Get
{
    public class GetStatusesListHandlerTests : BaseTest
    {
        [Fact]
        public async Task Handle_GetStatusesListTest()
        {
            var mockRepository = RepositoryMocks.GetStatusRepository();
            var handler = new GetStatusesListHandler(mockRepository, _mapper);
            var result = await handler.Handle(new GetStatusesListRequest(), CancellationToken.None);

            Assert.IsType<List<StatusDto>>(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void Handle_BadRequest()
        {
            var mockRepository = RepositoryMocks.GetStatusRepository();
            var handler = new GetStatusesListHandler(mockRepository, _mapper);

            Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }
    }
}
