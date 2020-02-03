using AutoMapper;
using Grasews.Domain.Entities;
using Grasews.Web.ViewModels;

namespace Grasews.Web.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, User_MvcCreateViewModel>().ReverseMap();

            CreateMap<User, Login_MvcViewModel>().ReverseMap()
                .ForMember(target => target.RegistrationDateTime, aux => aux.Ignore())
                .ForMember(target => target.Email, aux => aux.Ignore())
                .ForMember(target => target.FirstName, aux => aux.Ignore())
                .ForMember(target => target.LastName, aux => aux.Ignore())
                .ForMember(target => target.ServiceDescription_Users, aux => aux.Ignore());

            CreateMap<ShareInvitation_Acceptance_MvcViewModel, User>()
                .ForMember(target => target.Email, aux => aux.MapFrom(src => src.Email))
                .ForMember(target => target.FirstName, aux => aux.MapFrom(src => src.FirstName))
                .ForMember(target => target.LastName, aux => aux.MapFrom(src => src.LastName))
                .ForMember(target => target.Password, aux => aux.MapFrom(src => src.Password))
                .ForMember(target => target.Tasks, aux => aux.Ignore())
                .ForMember(target => target.ShareInvitations, aux => aux.Ignore())
                .ForMember(target => target.ServiceDescription_Users, aux => aux.Ignore())
                .ForMember(target => target.ServiceDescriptions, aux => aux.Ignore())
                .ForMember(target => target.Issues, aux => aux.Ignore());
        }
    }
}