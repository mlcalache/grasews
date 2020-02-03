using AutoMapper;
using Grasews.API.Models;
using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers.Extensions;

namespace Grasews.API.AutoMapper
{
    /// <summary>
    ///
    /// </summary>
    public class ShareInvitationAutoMapperProfile : Profile
    {
        /// <summary>
        ///
        /// </summary>
        public ShareInvitationAutoMapperProfile()
        {
            CreateMap<ShareInvitation, ShareInvitation_ApiResponseViewModel>()
              .ForMember(target => target.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(target => target.Email, opt => opt.MapFrom(src => src.Email))
              .ForMember(target => target.IdServiceDescription, opt => opt.MapFrom(src => src.IdServiceDescription))
              .ForMember(target => target.IdUserInviter, opt => opt.MapFrom(src => src.IdUserInviter))
              .ForMember(target => target.InvitationStatus, opt => opt.MapFrom(src => src.InvitationStatus))
              .ForMember(target => target.InvitationStatusDescription, opt => opt.MapFrom(src => src.InvitationStatus.GetEnumDescription()))
              .ForMember(target => target.RegistrationDateTime, opt => opt.MapFrom(src => src.RegistrationDateTime));

            CreateMap<ShareInvitation_ApiRequestUpdateModel, ShareInvitation>()
              .ForMember(target => target.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(target => target.Email, opt => opt.MapFrom(src => src.Email))
              .ForMember(target => target.IdServiceDescription, opt => opt.MapFrom(src => src.IdServiceDescription))
              .ForMember(target => target.IdUserInviter, opt => opt.MapFrom(src => src.IdUserInviter))
              .ForMember(target => target.InvitationStatus, opt => opt.MapFrom(src => src.InvitationStatus))
              .ForMember(target => target.ServiceDescription, opt => opt.Ignore())
              .ForMember(target => target.UserInviter, opt => opt.Ignore());

            CreateMap<ShareInvitationAccept_ApiRequestCreateModel, ShareInvitation>()
                .ForMember(target => target.Email, aux => aux.MapFrom(src => src.Email))
                .ForMember(target => target.IdServiceDescription, aux => aux.MapFrom(src => src.IdServiceDescription))
                .ForMember(target => target.IdUserInviter, aux => aux.MapFrom(src => src.IdUserInviter))
                .ForMember(target => target.InvitationStatus, aux => aux.Ignore())
                .ForMember(target => target.Id, aux => aux.Ignore())
                .ForMember(target => target.RegistrationDateTime, aux => aux.Ignore());

            CreateMap<ShareInvitationAccept_ApiRequestCreateModel, User>()
                .ForMember(target => target.Email, aux => aux.MapFrom(src => src.Email))
                .ForMember(target => target.Password, aux => aux.MapFrom(src => src.Password))
                .ForMember(target => target.Username, aux => aux.MapFrom(src => src.Username))
                .ForMember(target => target.IsAdmin, aux => aux.Ignore())
                .ForMember(target => target.FirstName, aux => aux.MapFrom(src => src.FirstName))
                .ForMember(target => target.LastName, aux => aux.MapFrom(src => src.LastName))
                .ForMember(target => target.FullName, aux => aux.Ignore())
                .ForMember(target => target.RegistrationDateTime, aux => aux.Ignore());
        }
    }
}