using AutoMapper;
using Grasews.Domain.Entities;
using Grasews.Web.ViewModels;

namespace Grasews.Web.AutoMapper
{
    public class OntologyProfile : Profile
    {
        public OntologyProfile()
        {
            CreateMap<Ontology, Ontology_MvcViewModel>();

            CreateMap<Ontology_MvcViewModel, Ontology>();
        }
    }
}