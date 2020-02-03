using AutoMapper;
using Grasews.API.Models;
using Grasews.Web.ViewModels;

namespace Grasews.Web.AutoMapper
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Task_ApiResponseViewModel, Task_MvcViewModel>()
                .ForMember(target => target.Comments, aux => aux.Ignore())
                .ForMember(target => target.Description, aux => aux.MapFrom(src => src.Description))
                .ForMember(target => target.Done, aux => aux.MapFrom(src => src.Done))
                .ForMember(target => target.Id, aux => aux.MapFrom(src => src.Id))
                .ForMember(target => target.IdServiceDescription, aux => aux.MapFrom(src => src.IdServiceDescription))
                .ForMember(target => target.IdOwnerUser, aux => aux.MapFrom(src => src.IdOwnerUser))
                .ForMember(target => target.RegistrationDateTime, aux => aux.MapFrom(src => src.RegistrationDateTime));

            CreateMap<TaskComment_ApiResponseViewModel, Task_Comment_MvcViewModel>()
                .ForMember(target => target.Comment, aux => aux.MapFrom(src => src.Comment))
                .ForMember(target => target.Id, aux => aux.MapFrom(src => src.Id))
                .ForMember(target => target.IdOwnerUser, aux => aux.MapFrom(src => src.IdOwnerUser))
                .ForMember(target => target.OwnerUserEmail, aux => aux.MapFrom(src => src.OwnerUserEmail))
                .ForMember(target => target.OwnerUsername, aux => aux.MapFrom(src => src.OwnerUsername))
                .ForMember(target => target.RegistrationDateTime, aux => aux.MapFrom(src => src.RegistrationDateTime));
        }
    }
}