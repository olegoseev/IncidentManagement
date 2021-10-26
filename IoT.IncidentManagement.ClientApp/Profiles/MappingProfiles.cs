using AutoMapper;

using IoT.IncidentManagement.ClientApp.Features.ClosureActions.Commands.Create;
using IoT.IncidentManagement.ClientApp.Features.Incidents.Commands.Create;
using IoT.IncidentManagement.ClientApp.Features.Incidents.Commands.Update;
using IoT.IncidentManagement.ClientApp.Features.ManagerActions.Create.Group;
using IoT.IncidentManagement.ClientApp.Features.ManagerActions.Get.Group;
using IoT.IncidentManagement.ClientApp.Features.Notes.Commands.Create;
using IoT.IncidentManagement.ClientApp.Features.Notifications.Create;
using IoT.IncidentManagement.ClientApp.Features.Notifications.Delete;
using IoT.IncidentManagement.ClientApp.Features.Notifications.Update;
using IoT.IncidentManagement.ClientApp.Features.Participants.Commands.Create;
using IoT.IncidentManagement.ClientApp.Features.Participants.Commands.Update;
using IoT.IncidentManagement.ClientApp.Models;
using IoT.IncidentManagement.ClientDomain.Entities;

namespace IoT.IncidentManagement.ClientApp.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            #region Incident mapping
            CreateMap<Incident, CreateIncidentRequest>()
                    .ForMember(dest => dest.BridgeId, opt => opt.MapFrom(src => src.Bridge.Id))
                    .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.Status.Id))
                    .ForMember(dest => dest.SeverityId, opt => opt.MapFrom(src => src.Severity.Id));
            CreateMap<CreateIncidentRequest, IncidentDto>();
            CreateMap<UpdateIncidentRequest, IncidentDto>();
            CreateMap<Incident, UpdateIncidentRequest>();
            #endregion

            #region participants mapping
            CreateMap<Participant, UpdateParticipantsRequest>();
            CreateMap<CreateParticipantsRequest, ParticipantsDto>();
            CreateMap<UpdateParticipantsRequest, ParticipantsDto>();
            #endregion

            #region create request to dto
            CreateMap<CreateNoteRequest, NoteDto>();
            CreateMap<CreateIncidentClosureActionsRequest, ClosureActionDto>();
            #endregion

            #region manageractions
            CreateMap<GetManagerActionGroupRequest, ManagerActionDto>();
            CreateMap<CreateManagerActionGroupRequest, ManagerActionDto>();
            #endregion

            #region notification mapping
            CreateMap<CreateNotificationGroupRequest, NotificationGroupDto>();
            CreateMap<DeleteNotificationGroupRequest, NotificationGroupDto>();
            CreateMap<UpdateNotificationRequest, NotificationDto>();
            CreateMap<NotificationDto, Notification>();
            CreateMap<Notification, UpdateNotificationRequest>();
            #endregion
        }
    }
}
