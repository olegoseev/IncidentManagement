using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Participants.Commands.Create;
using IoT.IncidentManagement.Application.UnitTests.Mocks;
using IoT.IncidentManagement.Contstants;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Participants.Create
{
    public class CreateParticipantHandlerTests : BaseTest
    {
        [Fact]
        public async Task Handle_AddNewParticipantToRepository()
        {
            var mockRepository = RepositoryMocks.GetParticipantRepository();
            var incidentRepository = RepositoryMocks.GetIncidentRepository();
            var handler = new CreateParticipantHandler(mockRepository, incidentRepository, _mapper);
            await handler.Handle(new CreateParticipantRequest { Group = "Participant1", IncidentId = 1 }, CancellationToken.None);

            var notes = (await mockRepository.GetAllAsync()).ToList();
            Assert.Equal(2, notes.Count);
            Assert.Contains(notes, b => b.Group == "Participant1");
        }

        [Fact]
        public async Task Handle_NullRequest()
        {
            var mockRepository = RepositoryMocks.GetParticipantRepository();
            var incidentRepository = RepositoryMocks.GetIncidentRepository();
            var handler = new CreateParticipantHandler(mockRepository, incidentRepository, _mapper);

            await Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public async Task Handle_RecordEmptyValidation()
        {
            var mockRepository = RepositoryMocks.GetParticipantRepository();
            var incidentRepository = RepositoryMocks.GetIncidentRepository();
            var handler = new CreateParticipantHandler(mockRepository, incidentRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new CreateParticipantRequest(), CancellationToken.None));
        }


        [Fact]
        public async Task Handle_DescriptionTooLongValidation()
        {
            var record = new string('A', ApplicationConstants.ParticipantMaxLen + 1);

            var mockRepository = RepositoryMocks.GetParticipantRepository();
            var incidentRepository = RepositoryMocks.GetIncidentRepository();
            var handler = new CreateParticipantHandler(mockRepository, incidentRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new CreateParticipantRequest
            {
                Group = record
            }, CancellationToken.None));
        }
    }
}
