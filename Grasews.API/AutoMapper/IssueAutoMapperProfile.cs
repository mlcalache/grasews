using AutoMapper;
using Grasews.API.Models;
using Grasews.Domain.Entities;

namespace Grasews.API.AutoMapper
{
    /// <summary>
    ///
    /// </summary>
    public class IssueAutoMapperProfile : Profile
    {
        /// <summary>
        ///
        /// </summary>
        public IssueAutoMapperProfile()
        {
            CreateMap<Issue, Issue_ApiResponseViewModel>()
               .ForMember(target => target.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(target => target.IdOwnerUser, opt => opt.MapFrom(src => src.IdOwnerUser))
               .ForMember(target => target.IdServiceDescription, opt => opt.MapFrom(src => src.IdServiceDescription))
               .ForMember(target => target.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(target => target.RegistrationDateTime, opt => opt.MapFrom(src => src.RegistrationDateTime))
               .ForMember(target => target.Solved, opt => opt.MapFrom(src => src.Solved))
               .ForMember(target => target.IdWsdlInterface, opt => opt.MapFrom(src => src.IdWsdlInterface))
               .ForMember(target => target.IdWsdlOperation, opt => opt.MapFrom(src => src.IdWsdlOperation))
               .ForMember(target => target.IdWsdlFault, opt => opt.MapFrom(src => src.IdWsdlFault))
               //.ForMember(target => target.IdWsdlInfault, opt => opt.MapFrom(src => src.IdWsdlInfault))
               //.ForMember(target => target.IdWsdlOutfault, opt => opt.MapFrom(src => src.IdWsdlOutfault))
               //.ForMember(target => target.IdWsdlInput, opt => opt.MapFrom(src => src.IdWsdlInput))
               //.ForMember(target => target.IdWsdlOutput, opt => opt.MapFrom(src => src.IdWsdlOutput))
               .ForMember(target => target.IdXsdComplexType, opt => opt.MapFrom(src => src.IdXsdComplexType))
               .ForMember(target => target.IdXsdSimpleType, opt => opt.MapFrom(src => src.IdXsdSimpleType))
               .ForMember(target => target.ServiceDescriptionElementType, opt => opt.Ignore())
               .ForMember(target => target.ServiceDescriptionElementLabel, opt => opt.MapFrom(src => GetServiceDescriptionElementLabel(src)));
            //.ForMember(target => target.ServiceDescriptionElementLabel, opt => opt.Ignore());

            CreateMap<Issue_ApiRequestCreateModel, Issue>()
               .ForMember(target => target.Id, opt => opt.Ignore())
               .ForMember(target => target.IdServiceDescription, opt => opt.MapFrom(src => src.IdServiceDescription))
               .ForMember(target => target.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(target => target.IssueAnswers, opt => opt.Ignore())
               .ForMember(target => target.OwnerUser, opt => opt.Ignore())
               .ForMember(target => target.RegistrationDateTime, opt => opt.Ignore())
               .ForMember(target => target.ServiceDescription, opt => opt.Ignore())
               .ForMember(target => target.IdOwnerUser, opt => opt.Ignore())
               .ForMember(target => target.Solved, opt => opt.Ignore())
               .ForMember(target => target.IdWsdlInterface, opt => opt.MapFrom(src => src.IdWsdlInterface))
               .ForMember(target => target.IdWsdlOperation, opt => opt.MapFrom(src => src.IdWsdlOperation))
               .ForMember(target => target.IdWsdlFault, opt => opt.MapFrom(src => src.IdWsdlFault))
               //.ForMember(target => target.IdWsdlInfault, opt => opt.MapFrom(src => src.IdWsdlInfault))
               //.ForMember(target => target.IdWsdlOutfault, opt => opt.MapFrom(src => src.IdWsdlOutfault))
               //.ForMember(target => target.IdWsdlInput, opt => opt.MapFrom(src => src.IdWsdlInput))
               //.ForMember(target => target.IdWsdlOutput, opt => opt.MapFrom(src => src.IdWsdlOutput))
               .ForMember(target => target.IdXsdComplexType, opt => opt.MapFrom(src => src.IdXsdComplexType))
               .ForMember(target => target.IdXsdSimpleType, opt => opt.MapFrom(src => src.IdXsdSimpleType));

            CreateMap<Issue_ApiRequestUpdateModel, Issue>()
               .ForMember(target => target.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(target => target.IdOwnerUser, opt => opt.Ignore())
               .ForMember(target => target.IdServiceDescription, opt => opt.MapFrom(src => src.IdServiceDescription))
               .ForMember(target => target.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(target => target.IssueAnswers, opt => opt.Ignore())
               .ForMember(target => target.OwnerUser, opt => opt.Ignore())
               .ForMember(target => target.RegistrationDateTime, opt => opt.Ignore())
               .ForMember(target => target.ServiceDescription, opt => opt.Ignore())
               .ForMember(target => target.Solved, opt => opt.MapFrom(src => src.Solved));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="issue"></param>
        /// <returns></returns>
        private static string GetServiceDescriptionElementLabel(Issue issue)
        {
            if (issue.IdWsdlInterface.HasValue && issue.WsdlInterface != null)
            {
                return issue.WsdlInterface.ToString();
            }
            else if (issue.IdWsdlOperation.HasValue && issue.WsdlOperation != null)
            {
                return issue.WsdlOperation.ToString();
            }
            else if (issue.IdWsdlFault.HasValue && issue.WsdlFault != null)
            {
                return issue.WsdlFault.ToString();
            }
            //else if (issue.IdWsdlInput.HasValue)
            //{
            //    return issue.WsdlInput.ToString();
            //}
            //else if (issue.IdWsdlOutput.HasValue)
            //{
            //    return issue.WsdlOutput.ToString();
            //}
            //else if (issue.IdWsdlInfault.HasValue)
            //{
            //    return issue.WsdlInfault.ToString();
            //}
            //else if (issue.IdWsdlOutfault.HasValue)
            //{
            //    return issue.WsdlOutfault.ToString();
            //}
            else if (issue.IdXsdComplexType.HasValue && issue.XsdComplexType != null)
            {
                return issue.XsdComplexType.ToString();
            }
            else if (issue.IdXsdSimpleType.HasValue && issue.XsdSimpleType != null)
            {
                return issue.XsdSimpleType.ToString();
            }

            return null;
        }
    }
}