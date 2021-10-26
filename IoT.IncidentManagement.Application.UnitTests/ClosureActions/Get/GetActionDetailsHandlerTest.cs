using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Get.Details;
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
    public class GetActionDetailsHandlerTest : BaseTest
    {
        [Theory]
        [InlineData(1, "Fist action")]
        [InlineData(2, "Second action")]
        [InlineData(3, "Third action")]
        [InlineData(4, "Forth action")]
        [InlineData(5, "Fifth action")]
        [InlineData(6, "Sixth action")]
        public async Task GetActionDetailsTest(int Id, string action)
        {
            var _mockActionRepository = RepositoryMocks.GetClosureActionRepository();
            var handler = new GetClosureActionDetailsHandler(_mockActionRepository, _mapper);

            var result = await handler.Handle(new GetClosureActionDetailsRequest { Id = Id }, CancellationToken.None);
            Assert.IsType<ClosureActionDto>(result);
            Assert.Equal(action, result.ToDoList);
        }


        [Fact]
        public void Handle_BadRequest()
        {
            var _mockActionRepository = RepositoryMocks.GetClosureActionRepository();
            var handler = new GetClosureActionDetailsHandler(_mockActionRepository, _mapper);

            Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public void Handle_NotFound()
        {
            var _mockActionRepository = RepositoryMocks.GetClosureActionRepository();
            var handler = new GetClosureActionDetailsHandler(_mockActionRepository, _mapper);

            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(new GetClosureActionDetailsRequest { Id = 55 }, CancellationToken.None));
        }
    }
}
