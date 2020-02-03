using AutoMapper;
using Grasews.API.Models;
using Grasews.Domain.Entities;

namespace Grasews.API.AutoMapper
{
    /// <summary>
    ///
    /// </summary>
    public class IssueAnswerAutoMapperProfile : Profile
    {
        /// <summary>
        ///
        /// </summary>
        public IssueAnswerAutoMapperProfile()
        {
            CreateMap<IssueAnswer, IssueAnswer_ApiResponseViewModel>()
                .ForMember(target => target.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(target => target.Answer, opt => opt.MapFrom(src => src.Answer))
                .ForMember(target => target.IdIssue, opt => opt.MapFrom(src => src.IdIssue))
                .ForMember(target => target.IdOwnerUser, opt => opt.MapFrom(src => src.IdOwnerUser))
                .ForMember(target => target.RegistrationDateTime, opt => opt.MapFrom(src => src.RegistrationDateTime))
                .ForMember(target => target.OwnerUserEmail, opt => opt.MapFrom(src => src.OwnerUser.Email))
                .ForMember(target => target.OwnerUsername, opt => opt.MapFrom(src => src.OwnerUser.Username));

            CreateMap<IssueAnswer_ApiRequestCreateModel, IssueAnswer>()
                .ForMember(target => target.Answer, opt => opt.MapFrom(src => src.Answer))
                .ForMember(target => target.IdIssue, opt => opt.Ignore())
                .ForMember(target => target.IdOwnerUser, opt => opt.Ignore())
                .ForMember(target => target.Id, opt => opt.Ignore())
                .ForMember(target => target.Issue, opt => opt.Ignore())
                .ForMember(target => target.OwnerUser, opt => opt.Ignore())
                .ForMember(target => target.RegistrationDateTime, opt => opt.Ignore());

            CreateMap<IssueAnswer_ApiRequestUpdateModel, IssueAnswer>()
                .ForMember(target => target.Answer, opt => opt.MapFrom(src => src.Answer))
                .ForMember(target => target.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(target => target.IdIssue, opt => opt.MapFrom(src => src.IdIssue))
                .ForMember(target => target.IdOwnerUser, opt => opt.Ignore())
                .ForMember(target => target.Issue, opt => opt.Ignore())
                .ForMember(target => target.OwnerUser, opt => opt.Ignore())
                .ForMember(target => target.RegistrationDateTime, opt => opt.Ignore());
        }
    }
}