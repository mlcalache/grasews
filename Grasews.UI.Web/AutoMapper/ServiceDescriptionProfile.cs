using AutoMapper;
using Grasews.API.Models;
using Grasews.Domain.Entities;
using Grasews.Web.ViewModels;

namespace Grasews.Web.AutoMapper
{
    public class ServiceDescriptionProfile : Profile
    {
        public ServiceDescriptionProfile()
        {
            CreateMap<ServiceDescription, ServiceDescription_MvcViewModel>()
                .ForMember(target => target.HasModelReference, aux => aux.Ignore())
                .ForMember(target => target.HasSchemaMapping, aux => aux.Ignore())
                .ForMember(target => target.HasLoweringSchemaMapping, aux => aux.Ignore())
                .ForMember(target => target.HasLiftingSchemaMapping, aux => aux.Ignore());

            CreateMap<ServiceDescription_MvcViewModel, ServiceDescription>();

            CreateMap<ServiceDescription_ApiResponseViewModel, ServiceDescription_RemoveAllSharing_MvcViewModel>()
                .ForMember(target => target.IdServiceDescription, aux => aux.MapFrom(src => src.Id))
                .ForMember(target => target.IdOwnerUser, aux => aux.MapFrom(src => src.IdOwnerUser))
                .ForMember(target => target.ServiceName, aux => aux.MapFrom(src => src.ServiceName));
        }
    }
}