using IoT.IncidentManagement.Application.Features.ManagerActions.Commands.Create.Group;
using IoT.IncidentManagement.Application.Features.ManagerActions.Commands.Create.One;
using IoT.IncidentManagement.Application.Features.ManagerActions.Commands.Delete;
using IoT.IncidentManagement.Application.Features.ManagerActions.Commands.Get.List;
using IoT.IncidentManagement.Application.Features.ManagerActions.Commands.Get.One;
using IoT.IncidentManagement.Application.Features.ManagerActions.Commands.Update;
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
    public class ManagerActionController : ControllerBase
    {
        private readonly IMediator mediator;

        public ManagerActionController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet("{id}/all", Name = "GetAllManagerActions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ManagerActionDto>>> GetAll(int id)
        {
            var dto = await mediator.Send(new GetManagerActionListRequest { IncidentId = id});
            return Ok(dto);
        }

        [HttpGet("{id}", Name = "GetManagerActionDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ManagerActionDto>> GetDetails(int id)
        {
            var dto = await mediator.Send(new GetManagerActionRequest { Id = id });
            return Ok(dto);
        }

        [HttpPost(Name = "CreateNewManagerAction")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Create([FromBody] CreateManagerActionRequest request)
        {
            var dto = await mediator.Send(request);
            return CreatedAtAction(nameof(GetDetails), new { id = dto.Id }, dto);
        }

        [HttpPost("group", Name = "CreateNewManagerActionGroup")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> CreateGroup([FromBody] CreateManagerActionGroupRequest request)
        {
            var dto = await mediator.Send(request);
            return CreatedAtAction(nameof(GetAll), new { id = request.IncidentId }, "");
        }


        [HttpPut(Name = "UpdateManagerAction")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateManagerActionRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteManagerAction")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteManagerActionRequest { Id = id });
            return NoContent();
        }
    }
}
