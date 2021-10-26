using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Create;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Application.UnitTests.Mocks;
using IoT.IncidentManagement.Contstants;
using IoT.IncidentManagement.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.ClosureActions.Create
{
    public class CreateActionHandlerTest : BaseTest
    {
        [Fact]
        public async Task Handle_AddNewActionToRepository()
        {
            var _mockActionRepository = RepositoryMocks.GetClosureActionRepository();
            var test = await _mockActionRepository.GetAllAsync();
            var handler = new CreateClosureActionHandler(_mockActionRepository, _mapper);
            await handler.Handle(new CreateClosureActionRequest { IncidentId = 1, ToDoList = "Twenty second" }, CancellationToken.None);

            var actions = (await _mockActionRepository.GetAllAsync()).ToList();
            Assert.Equal(7, actions.Count);
            Assert.Contains(actions, b => b.ToDoList == "Twenty second");
        }

        [Fact]
        public async Task Handle_NullRequest()
        {
            var _mockActionRepository = RepositoryMocks.GetClosureActionRepository();
            var handler = new CreateClosureActionHandler(_mockActionRepository, _mapper);

            await Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public async Task Handle_ActionTypeEmptyValidation()
        {
            var _mockActionRepository = RepositoryMocks.GetClosureActionRepository();
            var handler = new CreateClosureActionHandler(_mockActionRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new CreateClosureActionRequest(), CancellationToken.None));
        }


        [Fact]
        public async Task Handle_ActionTooLongValidation()
        {
            var action = new string('A', ApplicationConstants.ClosureActionMaxLen + 1);

            var _mockActionRepository = RepositoryMocks.GetClosureActionRepository();
            var handler = new CreateClosureActionHandler(_mockActionRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new CreateClosureActionRequest
            {
                ToDoList = action
            }, CancellationToken.None));
        }
    }
}
