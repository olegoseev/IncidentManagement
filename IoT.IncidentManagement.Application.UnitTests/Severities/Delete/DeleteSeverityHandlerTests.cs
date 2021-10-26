using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Severities.Commands.Delete;
using IoT.IncidentManagement.Application.Features.Severities.Commands.Get.List;
using IoT.IncidentManagement.Application.UnitTests.Mocks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Severities.Delete
{
    public class DeleteSeverityHandlerTests
    {
        public class DeleteSeverityHandlerTest : BaseTest
        {
            [Fact]
            public async Task Handle_BadRequest()
            {
                var _mockSeverityRepository = RepositoryMocks.GetSeverityRepository();
                var handler = new DeleteSeverityHandler(_mockSeverityRepository);

                await Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
            }


            [Fact]
            public async Task Handle_NotFound()
            {
                var _mockSeverityRepository = RepositoryMocks.GetSeverityRepository();
                var handler = new DeleteSeverityHandler(_mockSeverityRepository);

                await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(new DeleteSeverityRequest { Id = 55 }, CancellationToken.None));
            }

            [Fact]
            public async Task Handle_DeleteSeverityFromRepository()
            {

                var _mockSeverityRepository = RepositoryMocks.GetSeverityRepository();

                var getHandler = new GetSeveritiesListHandler(_mockSeverityRepository, _mapper);
                var result = await getHandler.Handle(new GetSeveritiesListRequest(), CancellationToken.None);

                var count = result.Count;

                var handler = new DeleteSeverityHandler(_mockSeverityRepository);
                await handler.Handle(new DeleteSeverityRequest { Id = 1 }, CancellationToken.None);

                getHandler = new GetSeveritiesListHandler(_mockSeverityRepository, _mapper);
                result = await getHandler.Handle(new GetSeveritiesListRequest(), CancellationToken.None);

                Assert.Equal(count - 1, result.Count);
            }
        }
    }
}
