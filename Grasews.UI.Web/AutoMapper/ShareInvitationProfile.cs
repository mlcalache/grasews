using AutoMapper;
using Grasews.API.Models;
using Grasews.Web.ViewModels;

namespace Grasews.Web.AutoMapper
{
    public class ShareInvitationProfile : Profile
    {
        public ShareInvitationProfile()
        {
            //CreateMap<ShareInvitationAcceptanceViewModel, ShareInvitation>()
            //    .ForMember(target => target.Email, aux => aux.MapFrom(src => src.Email))
            //    .ForMember(target => target.IdServiceDescription, aux => aux.MapFrom(src => src.IdServiceDescription))
            //    .ForMember(target => target.IdUserInviter, aux => aux.MapFrom(src => src.IdUserInviter))
            //    .ForMember(target => target.InvitationStatus, aux => aux.Ignore())
            //    .ForMember(target => target.Id, aux => aux.Ignore())
            //    .ForMember(target => target.RegistrationDateTime, aux => aux.Ignore());

            CreateMap<ShareInvitation_Acceptance_MvcViewModel, ShareInvitationAccept_ApiRequestCreateModel>();
        }
    }
}