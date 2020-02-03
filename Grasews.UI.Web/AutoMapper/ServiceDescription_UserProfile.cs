using AutoMapper;
using Grasews.API.Models;
using Grasews.Web.ViewModels;

namespace Grasews.Web.AutoMapper
{
    public class ServiceDescription_UserProfile : Profile
    {
        public ServiceDescription_UserProfile()
        {
            CreateMap<ServiceDescription_User_ApiResponseViewModel, ServiceDescription_User_MvcViewModel>()
                .ForMember(target => target.Id, aux => aux.MapFrom(src => src.Id))
                .ForMember(target => target.IdOwnerUser, aux => aux.MapFrom(src => src.IdOwnerUser))
                .ForMember(target => target.IdSharedUser, aux => aux.MapFrom(src => src.IdSharedUser))
                .ForMember(target => target.IdServiceDescription, aux => aux.MapFrom(src => src.IdServiceDescription))
                .ForMember(target => target.ServiceName, aux => aux.MapFrom(src => src.ServiceName))
                .ForMember(target => target.SharedUserEmail, opt => opt.MapFrom(src => src.SharedUserEmail))
                .ForMember(target => target.SharedUserFirstName, opt => opt.MapFrom(src => src.SharedUserFirstName))
                .ForMember(target => target.SharedUserLastName, opt => opt.MapFrom(src => src.SharedUserLastName))
                .ForMember(target => target.SharedUserUsername, opt => opt.MapFrom(src => src.SharedUserUsername));

            CreateMap<ServiceDescription_User_ApiResponseViewModel, ServiceDescription_RemoveSharing_MvcViewModel>()
                .ForMember(target => target.IdServiceDescription_User, aux => aux.MapFrom(src => src.Id))
                .ForMember(target => target.ServiceName, aux => aux.MapFrom(src => src.ServiceName))
                .ForMember(target => target.IdOwnerUser, aux => aux.MapFrom(src => src.IdOwnerUser))
                .ForMember(target => target.SharedUserEmail, aux => aux.MapFrom(src => src.Email));
        }
    }
}