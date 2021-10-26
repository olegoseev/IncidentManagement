using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Get.Details;
using IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Update;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Application.UnitTests.Mocks;
using IoT.IncidentManagement.Contstants;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.ClosureActions.Update
{
    public class UpdateActionHandlerTest : BaseTest
    {
        [Fact]
        public async Task Handle_UpdateActionHandlerTest()
        {
            var _mockActionRepository = RepositoryMocks.GetClosureActionRepository();
            var udpateHandler = new UpdateClosureActionHandler(_mockActionRepository, _mapper);
            await udpateHandler.Handle(new UpdateClosureActionRequest { Id = 3, ToDoList = "NEW Action" }, CancellationToken.None);


            var getHandler = new GetClosureActionDetailsHandler(_mockActionRepository, _mapper);

            var result = await getHandler.Handle(new GetClosureActionDetailsRequest { Id = 3 }, CancellationToken.None);
            Assert.IsType<ClosureActionDto>(result);
            Assert.Equal("NEW Action", result.ToDoList);
        }


        [Fact]
        public void Handle_BadRequest()
        {
            var _mockActionRepository = RepositoryMocks.GetClosureActionRepository();
            var handler = new UpdateClosureActionHandler(_mockActionRepository, _mapper);

            Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public void Handle_NotFound()
        {
            var _mockActionRepository = RepositoryMocks.GetClosureActionRepository();
            var handler = new UpdateClosureActionHandler(_mockActionRepository, _mapper);

            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(new UpdateClosureActionRequest { Id = 55 }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ActionTypeEmptyValidation()
        {
            var _mockActionRepository = RepositoryMocks.GetClosureActionRepository();
            var handler = new UpdateClosureActionHandler(_mockActionRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new UpdateClosureActionRequest { Id = 1 }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ActionTypeTooLongValidation()
        {
            var action = new string('A', ApplicationConstants.ClosureActionMaxLen + 1);
            var _mockActionRepository = RepositoryMocks.GetClosureActionRepository();
            var handler = new UpdateClosureActionHandler(_mockActionRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new UpdateClosureActionRequest
            {
                Id = 1,
                ToDoList = action
            }, CancellationToken.None));
        }
    }
}
