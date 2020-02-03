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
    public class ServiceDescriptionAutoMapperProfile : Profile
    {
        /// <summary>
        ///
        /// </summary>
        public ServiceDescriptionAutoMapperProfile()
        {
            CreateMap<ServiceDescription, ServiceDescription_ApiResponseViewModel>()
               .ForMember(target => target.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(target => target.GraphJson, opt => opt.MapFrom(src => src.GraphJson))
               .ForMember(target => target.IdOwnerUser, opt => opt.MapFrom(src => src.IdOwnerUser))
               .ForMember(target => target.RegistrationDateTime, opt => opt.MapFrom(src => src.RegistrationDateTime))
               .ForMember(target => target.ServiceName, opt => opt.MapFrom(src => src.ServiceName))
               .ForMember(target => target.Xml, opt => opt.MapFrom(src => src.Xml))
               .ForMember(target => target.WsdlInterfaces, opt => opt.MapFrom(src => src.WsdlInterfaces));

            CreateMap<ICollection<ServiceDescription>, ServiceDescriptionCollection_ApiResponseViewModel>()
                .ForMember(target => target.ServiceDescriptionViewModels, opt => opt.MapFrom(src => src));

            CreateMap<ServiceDescription, ServiceDescriptionCollection_ApiResponseViewModel.ServiceDescriptionViewModel>()
               .ForMember(target => target.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(target => target.IdOwnerUser, opt => opt.MapFrom(src => src.IdOwnerUser))
               .ForMember(target => target.RegistrationDateTime, opt => opt.MapFrom(src => src.RegistrationDateTime))
               .ForMember(target => target.ServiceName, opt => opt.MapFrom(src => src.ServiceName));

            CreateMap<ServiceDescription_ApiRequestCreateModel, ServiceDescription>()
                .ForMember(target => target.Id, opt => opt.Ignore())
                .ForMember(target => target.GraphJson, opt => opt.Ignore())
                .ForMember(target => target.Issues, opt => opt.Ignore())
                .ForMember(target => target.OwnerUser, opt => opt.Ignore())
                .ForMember(target => target.RegistrationDateTime, opt => opt.Ignore())
                .ForMember(target => target.ServiceDescription_Ontologies, opt => opt.Ignore())
                .ForMember(target => target.ServiceDescription_Users, opt => opt.Ignore())
                .ForMember(target => target.ShareInvitations, opt => opt.Ignore())
                .ForMember(target => target.Tasks, opt => opt.Ignore());

            CreateMap<ServiceDescription_ApiRequestUpdateModel, ServiceDescription>()
                .ForMember(target => target.Issues, opt => opt.Ignore())
                .ForMember(target => target.OwnerUser, opt => opt.Ignore())
                .ForMember(target => target.RegistrationDateTime, opt => opt.Ignore())
                .ForMember(target => target.ServiceDescription_Ontologies, opt => opt.Ignore())
                .ForMember(target => target.ServiceDescription_Users, opt => opt.Ignore())
                .ForMember(target => target.ShareInvitations, opt => opt.Ignore())
                .ForMember(target => target.Tasks, opt => opt.Ignore());

            CreateMap<ParseWsdlResponseDTO, ParseWsdl_ApiResponseViewModel>()
               .ForMember(target => target.WsdlInterfaces, opt => opt.MapFrom(src => src.ServiceDescription.WsdlInterfaces));

            CreateMap<WsdlInterface, ParseWsdl_ApiResponseViewModel.WsdlInterfaceResponseViewModel>().ReverseMap();

            CreateMap<WsdlOperation, ParseWsdl_ApiResponseViewModel.WsdlInterfaceResponseViewModel.WsdlOperationResponseViewModel>().ReverseMap();

            CreateMap<WsdlInfault, ParseWsdl_ApiResponseViewModel.WsdlInterfaceResponseViewModel.WsdlOperationResponseViewModel.WsdlInfaultResponseViewModel>().ReverseMap();

            CreateMap<WsdlInput, ParseWsdl_ApiResponseViewModel.WsdlInterfaceResponseViewModel.WsdlOperationResponseViewModel.WsdlInputResponseViewModel>().ReverseMap();

            CreateMap<WsdlOutfault, ParseWsdl_ApiResponseViewModel.WsdlInterfaceResponseViewModel.WsdlOperationResponseViewModel.WsdlOutfaultResponseViewModel>().ReverseMap();

            CreateMap<WsdlOutput, ParseWsdl_ApiResponseViewModel.WsdlInterfaceResponseViewModel.WsdlOperationResponseViewModel.WsdlOutputResponseViewModel>().ReverseMap();

            CreateMap<XsdComplexType, ParseWsdl_ApiResponseViewModel.XsdComplexTypeResponseViewModel>().ReverseMap();

            CreateMap<XsdSimpleType, ParseWsdl_ApiResponseViewModel.XsdSimpleTypeResponseViewModel>().ReverseMap();

            CreateMap<GraphJson_ApiRequestCreateModel, ParseWsdl_ApiRequestViewModel>();
        }
    }
}