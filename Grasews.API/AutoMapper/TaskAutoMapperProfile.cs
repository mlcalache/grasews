using AutoMapper;
using Grasews.API.Models;
using Grasews.Domain.Entities;

namespace Grasews.API.AutoMapper
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskAutoMapperProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public TaskAutoMapperProfile()
        {
            CreateMap<Task, Task_ApiResponseViewModel>()
              .ForMember(target => target.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(target => target.Done, opt => opt.MapFrom(src => src.Done))
              .ForMember(target => target.IdOwnerUser, opt => opt.MapFrom(src => src.IdOwnerUser))
              .ForMember(target => target.IdServiceDescription, opt => opt.MapFrom(src => src.IdServiceDescription))
              .ForMember(target => target.RegistrationDateTime, opt => opt.MapFrom(src => src.RegistrationDateTime))
              .ForMember(target => target.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<Task_ApiRequestCreateModel, Task>()
                .ForMember(target => target.Id, opt => opt.Ignore())
                .ForMember(target => target.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(target => target.Done, opt => opt.MapFrom(src => src.Done))
                .ForMember(target => target.IdOwnerUser, opt => opt.Ignore())
                .ForMember(target => target.IdServiceDescription, opt => opt.MapFrom(src => src.IdServiceDescription))
                .ForMember(target => target.ServiceDescription, opt => opt.Ignore())
                .ForMember(target => target.OwnerUser, opt => opt.Ignore())
                .ForMember(target => target.RegistrationDateTime, opt => opt.Ignore());

            CreateMap<Task_ApiRequestUpdateModel, Task>()
                .ForMember(target => target.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(target => target.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(target => target.Done, opt => opt.MapFrom(src => src.Done))
                .ForMember(target => target.IdOwnerUser, opt => opt.Ignore())
                .ForMember(target => target.IdServiceDescription, opt => opt.MapFrom(src => src.IdServiceDescription))
                .ForMember(target => target.ServiceDescription, opt => opt.Ignore())
                .ForMember(target => target.OwnerUser, opt => opt.Ignore())
                .ForMember(target => target.RegistrationDateTime, opt => opt.Ignore());
        }
    }
}