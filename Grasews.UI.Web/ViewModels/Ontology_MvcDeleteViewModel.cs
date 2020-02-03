using System.ComponentModel.DataAnnotations;

namespace Grasews.Web.ViewModels
{
    public class Ontology_MvcDeleteViewModel : BaseMvcModel<int>
    {
        [Display(Name = "OntologyName", ResourceType = typeof(WebResource))]
        public string OntologyName { get; set; }
    }
}