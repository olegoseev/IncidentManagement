using AutoMapper;

using IoT.IncidentManagement.Application.Features.Bridges.Commands.Create;
using IoT.IncidentManagement.Application.Features.Bridges.Commands.UpdateBridge;
using IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Create;
using IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Update;
using IoT.IncidentManagement.Application.Features.Incidents.Commands.Create;
using IoT.IncidentManagement.Application.Features.Incidents.Commands.Update;
using IoT.IncidentManagement.Application.Features.ManagerActions.Commands.Create.One;
using IoT.IncidentManagement.Application.Features.ManagerActions.Commands.Update;
using IoT.IncidentManagement.Application.Features.ManagerActions.Events;
using IoT.IncidentManagement.Application.Features.Notes.Commands.Create;
using IoT.IncidentManagement.Application.Features.Notes.Commands.Update;
using IoT.IncidentManagement.Application.Features.Notifications.Commands.Create.One;
using IoT.IncidentManagement.Application.Features.Notifications.Commands.Update;
using IoT.IncidentManagement.Application.Features.Notifications.Events;
using IoT.IncidentManagement.Application.Features.Participants.Commands.Create;
using IoT.IncidentManagement.Application.Features.Participants.Commands.Update;
using IoT.IncidentManagement.Application.Features.Severities.Commands.Create;
using IoT.IncidentManagement.Application.Features.Severities.Commands.Update;
using IoT.IncidentManagement.Application.Features.Statuses.Commands.Create;
using IoT.IncidentManagement.Application.Features.Statuses.Commands.Update;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

namespace IoT.IncidentManagement.BusinessModel.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Status mapping
            CreateMap<Status, StatusDto>().ReverseMap();
            CreateMap<CreateBridgeRequest, Status>();
            CreateMap<UpdateBridgeRequest, Status>();
            #endregion

            #region Bridge mapping
            CreateMap<Bridge, BridgeDto>().ReverseMap();
            CreateMap<CreateBridgeRequest, Bridge>();
            CreateMap<UpdateBridgeRequest, Bridge>();
            #endregion

            #region Incident mapping
            CreateMap<Incident, IncidentDto>()
                .ForMember(dest => dest.Bridge, opt => opt.MapFrom(src => src.Bridge))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Severity, opt => opt.MapFrom(src => src.Severity));
            CreateMap<Incident, IncidentExtDto>()
                .ForMember(dest => dest.Bridge, opt => opt.MapFrom(src => src.Bridge))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Severity, opt => opt.MapFrom(src => src.Severity))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.Participant, opt => opt.MapFrom(src => src.Participant.Group))
                .ForMember(dest => dest.ClosureAction, opt => opt.MapFrom(src => src.ClosureAction.ToDoList));

            CreateMap<CreateIncidentRequest, Incident>();
            CreateMap<UpdateIncidentRequest, Incident>();


            #endregion

            #region ClosureActions mapping
            CreateMap<ClosureActionDto, ClosureAction>().ReverseMap();
            CreateMap<CreateClosureActionRequest, ClosureAction>();
            CreateMap<UpdateClosureActionRequest, ClosureAction>();
            #endregion

            #region Note mapping
            CreateMap<NoteDto, Note>();
            CreateMap<Note, NoteDto>();
            CreateMap<CreateNoteRequest, Note>();
            CreateMap<UpdateNoteRequest, Note>();
            #endregion

            #region Participant mapping
            CreateMap<Participant, ParticipantDto>().ReverseMap();
            CreateMap<CreateParticipantRequest, Participant>();
            CreateMap<UpdateParticipantRequest, Participant>();
            #endregion

            #region Severity mapping
            CreateMap<Severity, SeverityDto>().ReverseMap();
            CreateMap<CreateSeverityRequest, Severity>();
            CreateMap<UpdateSeverityRequest, Severity>();
            #endregion

            #region Status mapping
            CreateMap<Status, StatusDto>();
            CreateMap<StatusDto, Status>();
            CreateMap<CreateStatusRequest, Status>();
            CreateMap<UpdateStatusRequest, Status>();
            #endregion

            #region Notification mapping
            CreateMap<Notification, NotificationDto>();
            //CreateMap<NotificationDto, Notification>();
            CreateMap<CreateNotificationRequest, Notification>();
            CreateMap<UpdateNotificationRequest, Notification>();
            //CreateMap<NotificationStore, CreateNotificationRequest>();
            CreateMap<CreateNotificationEvent, CreateNotificationRequest>();
            CreateMap<NotificationStore, CreateNotificationEvent>();
            #endregion

            #region ManagerAction
            CreateMap<ManagerAction, ManagerActionDto>();
            //CreateMap<ManagerActionDto, ManagerAction>();
            CreateMap<CreateManagerActionRequest, ManagerAction>();
            CreateMap<UpdateManagerActionRequest, ManagerAction>();
            CreateMap<ActionStore, CreateManagerActionRequest>();
            CreateMap<ManagerActionCreateEvent, CreateManagerActionRequest>();
            CreateMap<ActionStore, ManagerActionCreateEvent>();
            #endregion

        }
    }
}
