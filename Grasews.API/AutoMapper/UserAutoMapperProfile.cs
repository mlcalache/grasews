using AutoMapper;
using Grasews.API.Models;
using Grasews.Domain.Entities;

namespace Grasews.API.AutoMapper
{
    /// <summary>
    ///
    /// </summary>
    public class UserAutoMapperProfile : Profile
    {
        /// <summary>
        ///
        /// </summary>
        public UserAutoMapperProfile()
        {
            CreateMap<User, User_ApiResponseViewModel>()
              .ForMember(target => target.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(target => target.Email, opt => opt.MapFrom(src => src.Email))
              .ForMember(target => target.FirstName, opt => opt.MapFrom(src => src.FirstName))
              .ForMember(target => target.FullName, opt => opt.MapFrom(src => src.FullName))
              .ForMember(target => target.IsAdmin, opt => opt.MapFrom(src => src.IsAdmin))
              .ForMember(target => target.LastName, opt => opt.MapFrom(src => src.LastName))
              .ForMember(target => target.Password, opt => opt.MapFrom(src => src.Password))
              .ForMember(target => target.Username, opt => opt.MapFrom(src => src.Username))
              .ForMember(target => target.RegistrationDateTime, opt => opt.MapFrom(src => src.RegistrationDateTime));

            CreateMap<User_ApiRequestCreateModel, User>()
              .ForMember(target => target.Id, opt => opt.Ignore())
              .ForMember(target => target.Email, opt => opt.MapFrom(src => src.Email))
              .ForMember(target => target.FirstName, opt => opt.MapFrom(src => src.FirstName))
              .ForMember(target => target.IsAdmin, opt => opt.MapFrom(src => src.IsAdmin))
              .ForMember(target => target.FullName, opt => opt.Ignore())
              .ForMember(target => target.IssueAnswers, opt => opt.Ignore())
              .ForMember(target => target.Issues, opt => opt.Ignore())
              .ForMember(target => target.Ontologies, opt => opt.Ignore())
              .ForMember(target => target.ServiceDescriptions, opt => opt.Ignore())
              .ForMember(target => target.ServiceDescription_Users, opt => opt.Ignore())
              .ForMember(target => target.ShareInvitations, opt => opt.Ignore())
              .ForMember(target => target.TaskComments, opt => opt.Ignore())
              .ForMember(target => target.Tasks, opt => opt.Ignore())
              .ForMember(target => target.LastName, opt => opt.MapFrom(src => src.LastName))
              .ForMember(target => target.Username, opt => opt.MapFrom(src => src.Username));

            CreateMap<User_ApiRequestUpdateModel, User>()
              .ForMember(target => target.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(target => target.Email, opt => opt.MapFrom(src => src.Email))
              .ForMember(target => target.FirstName, opt => opt.MapFrom(src => src.FirstName))
              .ForMember(target => target.IsAdmin, opt => opt.MapFrom(src => src.IsAdmin))
              .ForMember(target => target.FullName, opt => opt.Ignore())
              .ForMember(target => target.IssueAnswers, opt => opt.Ignore())
              .ForMember(target => target.Issues, opt => opt.Ignore())
              .ForMember(target => target.Ontologies, opt => opt.Ignore())
              .ForMember(target => target.ServiceDescriptions, opt => opt.Ignore())
              .ForMember(target => target.ServiceDescription_Users, opt => opt.Ignore())
              .ForMember(target => target.ShareInvitations, opt => opt.Ignore())
              .ForMember(target => target.TaskComments, opt => opt.Ignore())
              .ForMember(target => target.Tasks, opt => opt.Ignore())
              .ForMember(target => target.LastName, opt => opt.MapFrom(src => src.LastName))
              .ForMember(target => target.Username, opt => opt.MapFrom(src => src.Username));

            CreateMap<ResetPassword, ResetPassword_ApiResponseModel>();

            //CreateMap<UserRequestCreateModel, UserResponseViewModel>()
            //  .ForMember(target => target.Email, opt => opt.MapFrom(src => src.Email))
            //  .ForMember(target => target.FirstName, opt => opt.MapFrom(src => src.FirstName))
            //  .ForMember(target => target.FullName, opt => opt.Ignore())
            //  .ForMember(target => target.Id, opt => opt.Ignore())
            //  .ForMember(target => target.IsAdmin, opt => opt.MapFrom(src => src.IsAdmin))
            //  .ForMember(target => target.LastName, opt => opt.MapFrom(src => src.LastName))
            //  .ForMember(target => target.Password, opt => opt.MapFrom(src => src.Password))
            //  .ForMember(target => target.RegistrationDateTime, opt => opt.Ignore())
            //  .ForMember(target => target.Username, opt => opt.MapFrom(src => src.Username));
        }
    }
}