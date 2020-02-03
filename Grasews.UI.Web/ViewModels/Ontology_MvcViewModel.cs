using System.Collections.Generic;

namespace Grasews.Web.ViewModels
{
    public class Ontology_MvcViewModel : BaseMvcModel<int>
    {
        public string OntologyName { get; set; }
        public User_MvcCreateViewModel OwnerUser { get; set; }
        public ICollection<ServiceDescription_Ontology_MvcResponseViewModel> WebService_Ontology { get; set; }
        public string Xml { get; set; }
    }
}