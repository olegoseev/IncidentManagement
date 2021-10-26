﻿using IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Create;
using IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Delete;
using IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Get.Details;
using IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Get.List;
using IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Update;
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
    public class ClosureActionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClosureActionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all", Name = "GetAllActions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClosureActionDto>>> GetAll()
        {
            var dto = await _mediator.Send(new GetClosureActionsListRequest());
            return Ok(dto);
        }


        [HttpGet("{id}", Name = "GetActionDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClosureActionDto>>> GetDetails(int id)
        {
            var dto = await _mediator.Send(new GetClosureActionDetailsRequest { Id = id });
            return Ok(dto);
        }


        [HttpPost(Name = "CreateNewAction")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Create([FromBody] CreateClosureActionRequest createActionRequest)
        {
            var dto = await _mediator.Send(createActionRequest);
            return CreatedAtAction(nameof(GetDetails), new { id = dto.IncidentId }, dto);
        }


        [HttpPut(Name = "UpdateAction")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateClosureActionRequest updateActionRequest)
        {
            await _mediator.Send(updateActionRequest);
            return NoContent();
        }


        [HttpDelete("{id}", Name = "DeleteAction")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteClosureActionRequest { Id = id });
            return NoContent();
        }
    }
}
