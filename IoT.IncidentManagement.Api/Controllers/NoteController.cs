using IoT.IncidentManagement.Application.Features.Notes.Commands.Create;
using IoT.IncidentManagement.Application.Features.Notes.Commands.Delete;
using IoT.IncidentManagement.Application.Features.Notes.Commands.Get.Details;
using IoT.IncidentManagement.Application.Features.Notes.Commands.Get.IncidentNotes;
using IoT.IncidentManagement.Application.Features.Notes.Commands.Update;
using IoT.IncidentManagement.Application.Models;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly IMediator mediator;

        public NoteController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet("{id}", Name = "GetNoteDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<NoteDto>>> GetDetails(int id)
        {
            var dto = await mediator.Send(new GetNoteDetailsRequest { Id = id });
            return Ok(dto);
        }


        [HttpGet("{id}/all", Name = "GetIncidentNotes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<NoteDto>>> GetIncidentNotes(int id)
        {
            var dto = await mediator.Send(new GetIncidentNotesRequest { IncidentId = id });
            return Ok(dto);
        }


        [HttpPost(Name = "CreateNewNote")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Create([FromBody] CreateNoteRequest createNoteRequest)
        {
            var dto = await mediator.Send(createNoteRequest);
            return CreatedAtAction(nameof(GetDetails), new { id = dto.Id }, dto);
        }

        [HttpPut(Name = "UpdateNote")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateNoteRequest updateNoteRequest)
        {
            await mediator.Send(updateNoteRequest);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteNote")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteNoteRequest { Id = id });
            return NoContent();
        }
    }
}
