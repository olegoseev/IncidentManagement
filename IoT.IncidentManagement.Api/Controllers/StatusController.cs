using IoT.IncidentManagement.Application.Features.Statuses.Commands.Create;
using IoT.IncidentManagement.Application.Features.Statuses.Commands.Delete;
using IoT.IncidentManagement.Application.Features.Statuses.Commands.Get.Details;
using IoT.IncidentManagement.Application.Features.Statuses.Commands.Get.List;
using IoT.IncidentManagement.Application.Features.Statuses.Commands.Update;
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
    public class StatusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all", Name = "GetAllStatuses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<StatusDto>>> GetAll()
        {
            var dto = await _mediator.Send(new GetStatusesListRequest());
            return Ok(dto);
        }


        [HttpGet("{id}", Name = "GetStatusDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<StatusDto>>> GetDetails(int id)
        {
            var dto = await _mediator.Send(new GetStatusDetailsRequest { Id = id });
            return Ok(dto);
        }


        [HttpPost(Name = "CreateNewStatus")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Create([FromBody] CreateStatusRequest createStatusRequest)
        {
            var dto = await _mediator.Send(createStatusRequest);
            return CreatedAtAction(nameof(GetDetails), new { id = dto.Id }, dto);
        }


        [HttpPut(Name = "UpdateStatus")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateStatusRequest updateStatusRequest)
        {
            await _mediator.Send(updateStatusRequest);
            return NoContent();
        }


        [HttpDelete("{id}", Name = "DeleteStatus")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteStatusRequest { Id = id });
            return NoContent();
        }
    }
}
