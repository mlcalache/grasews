namespace Grasews.Web.ViewModels
{
    public class ServiceDescription_Ontology_MvcResponseViewModel : BaseMvcModel<int>
    {
        public Ontology_MvcViewModel Ontology { get; set; }
        public ServiceDescription_MvcViewModel WebService { get; set; }
    }
}