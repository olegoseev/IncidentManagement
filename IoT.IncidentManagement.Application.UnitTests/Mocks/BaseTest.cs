using AutoMapper;

using IoT.IncidentManagement.BusinessModel.Profiles;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.UnitTests.Mocks
{
    public class BaseTest
    {
        protected readonly IMapper _mapper;
        public BaseTest()
        {
            var configurationProvider = new MapperConfiguration(config =>
            {
                config.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }
    }
}
