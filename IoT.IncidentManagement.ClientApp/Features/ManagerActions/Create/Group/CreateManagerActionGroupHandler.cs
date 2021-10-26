﻿using AutoMapper;

using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientApp.Exceptions;
using IoT.IncidentManagement.ClientApp.Models;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.ManagerActions.Create.Group
{
    public class CreateManagerActionGroupHandler : IRequestHandler<CreateManagerActionGroupRequest>
    {
        private readonly IManagerActionClient client;
        private readonly IMapper mapper;

        public CreateManagerActionGroupHandler(IManagerActionClient client, IMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(CreateManagerActionGroupRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new BadRequestException(nameof(request));

            var validator = new CreateManagerActionGroupValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid is false)
                throw new ValidationException(validationResult);


            var dto = mapper.Map<ManagerActionDto>(request);
            await client.AddGroupAsync(dto, cancellationToken);
            return Unit.Value;
        }
    }
}
