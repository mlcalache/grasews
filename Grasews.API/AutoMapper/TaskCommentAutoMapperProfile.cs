using AutoMapper;
using Grasews.API.Models;
using Grasews.Domain.Entities;

namespace Grasews.API.AutoMapper
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskCommentAutoMapperProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public TaskCommentAutoMapperProfile()
        {
            CreateMap<TaskComment, TaskComment_ApiResponseViewModel>()
              .ForMember(target => target.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(target => target.Comment, opt => opt.MapFrom(src => src.Comment))
              .ForMember(target => target.IdOwnerUser, opt => opt.MapFrom(src => src.IdOwnerUser))
              .ForMember(target => target.IdTask, opt => opt.MapFrom(src => src.IdTask))
              .ForMember(target => target.RegistrationDateTime, opt => opt.MapFrom(src => src.RegistrationDateTime))
              .ForMember(target => target.OwnerUserEmail, opt => opt.MapFrom(src => src.OwnerUser.Email))
              .ForMember(target => target.OwnerUsername, opt => opt.MapFrom(src => src.OwnerUser.Username));

            CreateMap<TaskComment_ApiRequestCreateModel, TaskComment>()
                .ForMember(target => target.Id, opt => opt.Ignore())
                .ForMember(target => target.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(target => target.IdOwnerUser, opt => opt.Ignore())
                .ForMember(target => target.IdTask, opt => opt.MapFrom(src => src.IdTask))
                .ForMember(target => target.Task, opt => opt.Ignore())
                .ForMember(target => target.OwnerUser, opt => opt.Ignore())
                .ForMember(target => target.RegistrationDateTime, opt => opt.Ignore());

            CreateMap<TaskComment_ApiRequestUpdateModel, TaskComment>()
                .ForMember(target => target.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(target => target.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(target => target.IdOwnerUser, opt => opt.Ignore())
                .ForMember(target => target.IdTask, opt => opt.MapFrom(src => src.IdTask))
                .ForMember(target => target.Task, opt => opt.Ignore())
                .ForMember(target => target.OwnerUser, opt => opt.Ignore())
                .ForMember(target => target.RegistrationDateTime, opt => opt.Ignore());
        }
    }
}