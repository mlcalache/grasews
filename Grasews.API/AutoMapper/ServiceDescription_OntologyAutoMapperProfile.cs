using AutoMapper;
using Grasews.API.Models;
using Grasews.Domain.Entities;

namespace Grasews.API.AutoMapper
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceDescription_OntologyAutoMapperProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public ServiceDescription_OntologyAutoMapperProfile()
        {
            CreateMap<ServiceDescription_Ontology, ServiceDescription_Ontology_ApiResponseViewModel>()
               .ForMember(target => target.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(target => target.IdOntology, opt => opt.MapFrom(src => src.IdOntology))
               .ForMember(target => target.IdServiceDescription, opt => opt.MapFrom(src => src.IdServiceDescription))
               .ForMember(target => target.RegistrationDateTime, opt => opt.MapFrom(src => src.RegistrationDateTime));

            CreateMap<ServiceDescription_Ontology_ApiRequestCreateModel, ServiceDescription_Ontology>()
               .ForMember(target => target.Id, opt => opt.Ignore())
               .ForMember(target => target.IdOntology, opt => opt.MapFrom(src => src.IdOntology))
               .ForMember(target => target.IdServiceDescription, opt => opt.MapFrom(src => src.IdServiceDescription));
        }
    }
}