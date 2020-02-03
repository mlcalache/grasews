using AutoMapper;
using Grasews.API.Models;
using Grasews.Application.DTOs;
using Grasews.Domain.Entities;
using System.Collections.Generic;

namespace Grasews.API.AutoMapper
{
    /// <summary>
    ///
    /// </summary>
    public class OntologyAutoMapperProfile : Profile
    {
        /// <summary>
        ///
        /// </summary>
        public OntologyAutoMapperProfile()
        {
            CreateMap<Ontology, Ontology_ApiResponseViewModel>()
                .ForMember(target => target.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(target => target.IdOwnerUser, opt => opt.MapFrom(src => src.IdOwnerUser))
                .ForMember(target => target.OntologyName, opt => opt.MapFrom(src => src.OntologyName))
                .ForMember(target => target.RegistrationDateTime, opt => opt.MapFrom(src => src.RegistrationDateTime))
                .ForMember(target => target.Xml, opt => opt.MapFrom(src => src.Xml));

            CreateMap<Ontology_ApiRequestCreateModel, Ontology>()
                .ForMember(target => target.Id, opt => opt.Ignore())
                .ForMember(target => target.OwnerUser, opt => opt.Ignore())
                .ForMember(target => target.RegistrationDateTime, opt => opt.Ignore())
                .ForMember(target => target.ServiceDescription_Ontologies, opt => opt.Ignore())
                .ForMember(target => target.OntologyName, opt => opt.Ignore())
                .ForMember(target => target.Xml, opt => opt.MapFrom(src => src.Xml))
                .ForMember(target => target.IdOwnerUser, opt => opt.Ignore());

            CreateMap<ICollection<Ontology>, OntologyCollection_ApiResponseViewModel>()
                .ForMember(target => target.OntologyViewModels, opt => opt.MapFrom(src => src));

            CreateMap<Ontology, Ontology_ApiResponseViewModel>()
                .ForMember(target => target.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(target => target.IdOwnerUser, opt => opt.MapFrom(src => src.IdOwnerUser))
                .ForMember(target => target.OntologyName, opt => opt.MapFrom(src => src.OntologyName))
                .ForMember(target => target.OntologyTerms, opt => opt.MapFrom(src => src.OntologyTerms))
                .ForMember(target => target.RegistrationDateTime, opt => opt.MapFrom(src => src.RegistrationDateTime))
                .ForMember(target => target.Xml, opt => opt.MapFrom(src => src.Xml))
                .ForMember(target => target.OntologyUrl, opt => opt.MapFrom(src => src.OntologyUrl));

            CreateMap<ParseOwlResponseDTO, ParseOwl_ApiResponseViewModel>();

            CreateMap<OntologyTerm, FindTerm_ApiResponseViewModel>();

            CreateMap<OntologyTerm, OntologyTermFindPath_ApiResponseViewModel>();
        }
    }
}