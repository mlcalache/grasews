using AutoMapper;
using Grasews.API.Models;
using Grasews.Web.ViewModels;

namespace Grasews.Web.AutoMapper
{
    public class IssueProfile : Profile
    {
        public IssueProfile()
        {
            CreateMap<Issue_ApiResponseViewModel, Issue_MvcViewModel>()
               .ForMember(target => target.Answers, aux => aux.Ignore())
               .ForMember(target => target.Description, aux => aux.MapFrom(src => src.Description))
               .ForMember(target => target.Solved, aux => aux.MapFrom(src => src.Solved))
               .ForMember(target => target.Id, aux => aux.MapFrom(src => src.Id))
               .ForMember(target => target.IdServiceDescription, aux => aux.MapFrom(src => src.IdServiceDescription))
               .ForMember(target => target.IdOwnerUser, aux => aux.MapFrom(src => src.IdOwnerUser))
               .ForMember(target => target.RegistrationDateTime, aux => aux.MapFrom(src => src.RegistrationDateTime));

            CreateMap<IssueAnswer_ApiResponseViewModel, Issue_Answer_MvcViewModel>()
                .ForMember(target => target.Answer, aux => aux.MapFrom(src => src.Answer))
                .ForMember(target => target.Id, aux => aux.MapFrom(src => src.Id))
                .ForMember(target => target.IdOwnerUser, aux => aux.MapFrom(src => src.IdOwnerUser))
                .ForMember(target => target.OwnerUserEmail, aux => aux.MapFrom(src => src.OwnerUserEmail))
                .ForMember(target => target.OwnerUsername, aux => aux.MapFrom(src => src.OwnerUsername))
                .ForMember(target => target.RegistrationDateTime, aux => aux.MapFrom(src => src.RegistrationDateTime));
        }
    }
}