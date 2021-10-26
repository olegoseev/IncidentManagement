using IoT.IncidentManagement.Application.Features.Severities.Commands.Create;
using IoT.IncidentManagement.Application.Features.Severities.Commands.Delete;
using IoT.IncidentManagement.Application.Features.Severities.Commands.Get.Details;
using IoT.IncidentManagement.Application.Features.Severities.Commands.Get.List;
using IoT.IncidentManagement.Application.Features.Severities.Commands.Update;
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
    public class SeverityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SeverityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all", Name = "GetAllSeverities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SeverityDto>>> GetAll()
        {
            var dto = await _mediator.Send(new GetSeveritiesListRequest());
            return Ok(dto);
        }


        [HttpGet("{id}", Name = "GetSeverityDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SeverityDto>>> GetDetails(int id)
        {
            var dto = await _mediator.Send(new GetSeverityDetailsRequest { Id = id });
            return Ok(dto);
        }


        [HttpPost(Name = "CreateNewSeverity")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Create([FromBody] CreateSeverityRequest createSeverityRequest)
        {
            var dto = await _mediator.Send(createSeverityRequest);
            return CreatedAtAction(nameof(GetDetails), new { id = dto.Id }, dto);
        }


        [HttpPut(Name = "UpdateSeverity")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateSeverityRequest updateSeverityRequest)
        {
            await _mediator.Send(updateSeverityRequest);
            return NoContent();
        }


        [HttpDelete("{id}", Name = "DeleteSeverity")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteSeverityRequest { Id = id });
            return NoContent();
        }
    }
}
