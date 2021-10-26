using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Statuses.Commands.Get.Details;
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
    public class GetStatusDetailsHandlerTests : BaseTest
    {
        [Theory]
        [InlineData(1, "status1")]
        [InlineData(2, "status2")]
        [InlineData(3, "status3")]
        public async Task GetActionDetailsTest(int id, string status)
        {
            var mockRepository = RepositoryMocks.GetStatusRepository();
            var handler = new GetStatusDetailsHandler(mockRepository, _mapper);

            var result = await handler.Handle(new GetStatusDetailsRequest { Id = id }, CancellationToken.None);
            Assert.IsType<StatusDto>(result);
            Assert.Equal(status, result.CurrentStatus);
        }


        [Fact]
        public void Handle_BadRequest()
        {
            var mockRepository = RepositoryMocks.GetStatusRepository();
            var handler = new GetStatusDetailsHandler(mockRepository, _mapper);

            Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public void Handle_NotFound()
        {
            var mockRepository = RepositoryMocks.GetStatusRepository();
            var handler = new GetStatusDetailsHandler(mockRepository, _mapper);

            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(new GetStatusDetailsRequest { Id = 55 }, CancellationToken.None));
        }
    }
}
