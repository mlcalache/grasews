using AutoMapper;
using Grasews.API.Models;
using Grasews.Domain.Entities;

namespace Grasews.API.AutoMapper
{
    /// <summary>
    ///
    /// </summary>
    public class ServiceDescription_UserAutoMapperProfile : Profile
    {
        /// <summary>
        ///
        /// </summary>
        public ServiceDescription_UserAutoMapperProfile()
        {
            CreateMap<ServiceDescription_User, ServiceDescription_User_ApiResponseViewModel>()
               .ForMember(target => target.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(target => target.IdServiceDescription, opt => opt.MapFrom(src => src.IdServiceDescription))
               .ForMember(target => target.IdSharedUser, opt => opt.MapFrom(src => src.IdSharedUser))
               .ForMember(target => target.RegistrationDateTime, opt => opt.MapFrom(src => src.RegistrationDateTime))
               .ForMember(target => target.ServiceName, opt => opt.MapFrom(src => src.ServiceDescription.ServiceName))
               .ForMember(target => target.Email, opt => opt.MapFrom(src => src.SharedUser.Email))
               .ForMember(target => target.IdOwnerUser, opt => opt.MapFrom(src => src.ServiceDescription.IdOwnerUser))
               .ForMember(target => target.SharedUserEmail, opt => opt.MapFrom(src => src.SharedUser.Email))
               .ForMember(target => target.SharedUserFirstName, opt => opt.MapFrom(src => src.SharedUser.FirstName))
               .ForMember(target => target.SharedUserLastName, opt => opt.MapFrom(src => src.SharedUser.LastName))
               .ForMember(target => target.SharedUserUsername, opt => opt.MapFrom(src => src.SharedUser.Username));

            CreateMap<ServiceDescription_User_ApiRequestCreateModel, ServiceDescription_User>()
                .ForMember(target => target.Id, opt => opt.Ignore())
                .ForMember(target => target.IdServiceDescription, opt => opt.MapFrom(src => src.IdServiceDescription))
                .ForMember(target => target.IdSharedUser, opt => opt.MapFrom(src => src.IdSharedUser))
                .ForMember(target => target.RegistrationDateTime, opt => opt.Ignore())
                .ForMember(target => target.ServiceDescription, opt => opt.Ignore())
                .ForMember(target => target.SharedUser, opt => opt.Ignore());

            CreateMap<ServiceDescription_User_ApiRequestUpdateModel, ServiceDescription_User>()
                .ForMember(target => target.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(target => target.IdServiceDescription, opt => opt.MapFrom(src => src.IdServiceDescription))
                .ForMember(target => target.IdSharedUser, opt => opt.MapFrom(src => src.IdSharedUser))
                .ForMember(target => target.RegistrationDateTime, opt => opt.Ignore())
                .ForMember(target => target.ServiceDescription, opt => opt.Ignore())
                .ForMember(target => target.SharedUser, opt => opt.Ignore());
        }
    }
}