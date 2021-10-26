using IoT.IncidentManagement.Application.Features.Participants.Commands.Create;
using IoT.IncidentManagement.Application.Features.Participants.Commands.Delete;
using IoT.IncidentManagement.Application.Features.Participants.Commands.Get.Details;
using IoT.IncidentManagement.Application.Features.Participants.Commands.Get.IncidentAttendees;
using IoT.IncidentManagement.Application.Features.Participants.Commands.Update;
using IoT.IncidentManagement.Application.Models;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace IoT.IncidentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly IMediator mediator;

        public ParticipantController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        //[HttpGet("{id}", Name = "GetParticipantDetails")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<ParticipantDto>> GetDetails(int id)
        //{
        //    var dto = await _mediator.Send(new GetParticipantDetailsRequest { Id = id });
        //    return Ok(dto);
        //}


        [HttpGet("{id}", Name = "GetIncidentParticipants")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ParticipantDto>> GetIncidentParticipants(int id)
        {
            var dto = await mediator.Send(new GetIncidentParticipantsRequest { IncidentId = id });
            return Ok(dto);
        }


        [HttpPost(Name = "CreateNewParticipant")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Create([FromBody] CreateParticipantRequest createParticipantRequest)
        {
            var dto = await mediator.Send(createParticipantRequest);
            return CreatedAtAction(nameof(GetIncidentParticipants), new { id = dto.IncidentId }, dto);
        }

        [HttpPut(Name = "UpdateParticipant")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateParticipantRequest updateParticipantRequest)
        {
            await mediator.Send(updateParticipantRequest);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteParticipant")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteParticipantRequest { IncidentId = id });
            return NoContent();
        }
    }
}
