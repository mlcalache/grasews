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
    public class SawsdlAutoMapperProfile : Profile
    {
        /// <summary>
        ///
        /// </summary>
        public SawsdlAutoMapperProfile()
        {
            CreateMap<SawsdlModelReference_ApiRequestCreateModel, SawsdlModelReferenceRequestCreateDTO>();

            CreateMap<SawsdlLiftingSchemaMapping_ApiRequestCreateModel, SawsdlLiftingSchemaMappingRequestCreateDTO>();

            CreateMap<SawsdlLoweringSchemaMapping_ApiRequestCreateModel, SawsdlLoweringSchemaMappingRequestCreateDTO>();

            CreateMap<ICollection<SawsdlModelReference>, SawsdlModelReferenceCollection_ApiRequestViewModel>()
                .ForMember(target => target.SawsdlModelReferenceViewModels, opt => opt.MapFrom(src => src));

            CreateMap<SawsdlModelReference, SawsdlModelReferenceCollection_ApiRequestViewModel.ModelReferenceViewModel>()
               .ForMember(target => target.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(target => target.IdServiceDescription, opt => opt.MapFrom(src => src.IdServiceDescription))
               .ForMember(target => target.IdOntologyTerm, opt => opt.MapFrom(src => src.IdOntologyTerm))
               .ForMember(target => target.ModelReference, opt => opt.MapFrom(src => src.ModelReference));
        }
    }
}