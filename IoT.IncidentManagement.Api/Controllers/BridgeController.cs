using IoT.IncidentManagement.Application.Features.Bridges.Commands.Create;
using IoT.IncidentManagement.Application.Features.Bridges.Commands.DeleteBridge;
using IoT.IncidentManagement.Application.Features.Bridges.Commands.Get.Details;
using IoT.IncidentManagement.Application.Features.Bridges.Commands.Get.List;
using IoT.IncidentManagement.Application.Features.Bridges.Commands.UpdateBridge;
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
    public class BridgeController : ControllerBase
    {
        private readonly IMediator mediator;

        public BridgeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("all", Name = "GetAllBridges")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<BridgeDto>>> GetAll()
        {
            var dto = await mediator.Send(new GetBridgesListRequest());
            return Ok(dto);
        }


        [HttpGet("{id}", Name = "GetBridgeDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<BridgeDto>>> GetDetails(int id)
        {
            var dto = await mediator.Send(new GetBridgeDetailsRequest { Id = id });
            return Ok(dto);
        }


        [HttpPost(Name = "CreateNewBridge")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Create([FromBody] CreateBridgeRequest createBridgeRequest)
        {
            var dto = await mediator.Send(createBridgeRequest);
            return CreatedAtAction(nameof(GetDetails), new { id = dto.Id }, dto);
        }


        [HttpPut(Name = "UpdateBridge")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateBridgeRequest updateBridgeRequest)
        {
            await mediator.Send(updateBridgeRequest);
            return NoContent();
        }


        [HttpDelete("{id}", Name = "DeleteBridge")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteBridgeRequest { Id = id });
            return NoContent();
        }
    }
}
