using IoT.IncidentManagement.Application.Features.Incidents.Commands.Create;
using IoT.IncidentManagement.Application.Features.Incidents.Commands.Delete;
using IoT.IncidentManagement.Application.Features.Incidents.Commands.Get.Details;
using IoT.IncidentManagement.Application.Features.Incidents.Commands.Get.DetailsExtended;
using IoT.IncidentManagement.Application.Features.Incidents.Commands.Get.List;
using IoT.IncidentManagement.Application.Features.Incidents.Commands.Update;
using IoT.IncidentManagement.Application.Features.ManagerActions.Commands.Create.Group;
using IoT.IncidentManagement.Application.Features.Notes.Commands.Create;
using IoT.IncidentManagement.Application.Features.Notifications.Commands.Create.Group;
using IoT.IncidentManagement.Application.Features.Notifications.Commands.Delete.Group;
using IoT.IncidentManagement.Application.Features.Notifications.Commands.Get.Types;
using IoT.IncidentManagement.Application.Features.Participants.Commands.Create;
using IoT.IncidentManagement.Application.Features.Severities.Commands.Get.Details;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Enums;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentController : ControllerBase
    {
        private readonly IMediator mediator;

        public IncidentController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost(Name = "CreateNewIncident")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Create([FromBody] CreateIncidentRequest createIncidentRequest)
        {
            var dto = await mediator.Send(createIncidentRequest);
            return CreatedAtAction(nameof(GetIncidentDetailsById), new { dto.Id }, dto);
        }

        [HttpPut(Name = "UpdateIncident")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateIncidentRequest updateIncidentRequest)
        {
            await mediator.Send(updateIncidentRequest);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteIncident")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteIncidentRequest { Id = id });
            return NoContent();
        }

        [HttpGet("{id}", Name = "GetIncidentDetailsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IncidentDto>> GetIncidentDetailsById(int id)
        {
            var dto = await mediator.Send(new GetIncidentDetailsRequest { Id = id });
            return Ok(dto);
        }

        [HttpGet("{id}/extended", Name = "GetIncidentExtendedDetailsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IncidentExtDto>> GetIncidentExtendedDetailsById(int id)
        {
            var dto = await mediator.Send(new GetIncidentDetailsExtRequest { Id = id });
            return Ok(dto);
        }


        [HttpGet(Name = "GetAllIncidents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<IncidentDto>>> GetAllIncidents()
        {
            var dto = await mediator.Send(new GetIncidentsListRequest());
            return Ok(dto);
        }
    }
}
