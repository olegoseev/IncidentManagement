using IoT.IncidentManagement.Application.Features.Notifications.Commands.Create.Group;
using IoT.IncidentManagement.Application.Features.Notifications.Commands.Create.One;
using IoT.IncidentManagement.Application.Features.Notifications.Commands.Delete.Group;
using IoT.IncidentManagement.Application.Features.Notifications.Commands.Delete.One;
using IoT.IncidentManagement.Application.Features.Notifications.Commands.Get.Group;
using IoT.IncidentManagement.Application.Features.Notifications.Commands.Get.List;
using IoT.IncidentManagement.Application.Features.Notifications.Commands.Get.One;
using IoT.IncidentManagement.Application.Features.Notifications.Commands.Get.Types;
using IoT.IncidentManagement.Application.Features.Notifications.Commands.Update;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Enums;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace IoT.IncidentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator mediator;

        public NotificationController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotificationDto>> GetNotification(int id)
        {
            if (id == 0)
                throw new ArgumentException($"{id} must be grater than 0", nameof(id));

            var dto = await mediator.Send(new GetNotificationRequest { Id = id });
            return Ok(dto);
        }



        [HttpGet("{id}/all", Name = "GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<NotificationDto>>> GetIncidentNotifications(int id)
        {
            if (id == 0)
                throw new ArgumentException($"{id} must be grater than 0", nameof(id));

            var dto = await mediator.Send(new GetNotificationListRequest { IncidentId = id });
            return Ok(dto);
        }


        [HttpGet("{id}/registered", Name = "GetIncidentRegisteredNotificationGroups")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IncidentNotificationGroup>> GetIncidentRegisteredNotificationGroups(int id)
        {
            if (id == 0)
                throw new ArgumentException($"{id} must be grater than 0", nameof(id));

            var dto = await mediator.Send(new GetRegisteredNotificationGroupsRequest { IncidentId = id });
            return Ok(dto);
        }


        [HttpGet("{id}/group/{group}", Name = "GetIncidentNotificationGroups")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IncidentNotificationGroup>> GetIncidentNotificationGroups(int id, NotificationGroup group)
        {
            if (id == 0)
                throw new ArgumentException($"{id} must be grater than 0", nameof(id));

            var dto = await mediator.Send(new GetIncidentNotificationGroupRequest { IncidentId = id, Group = group });
            return Ok(dto);
        }



        [HttpPost(Name = "CreateNewNotification")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<NotificationDto>> Create([FromBody] CreateNotificationRequest request)
        {
            var dto = await mediator.Send(request);
            return dto;
        }


        [HttpPost("group", Name = "CreateNewNotificationGroup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> CreateGroup([FromBody] CreateNotificationGroupRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }


        [HttpPut(Name = "UpdateNotification")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put([FromBody] UpdateNotificationRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }


        [HttpDelete("{id}", Name = "DeleteNotification")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteNotificationRequest { Id = id });
            return NoContent();
        }



        [HttpDelete("{id}/group/{group}", Name = "DeleteNotificatinGroup")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteGroup(int id, NotificationGroup group)
        {
            await mediator.Send(new DeleteNotificationGroupRequest { IncidentId = id, Group = group });
            return NoContent();
        }
    }
}
