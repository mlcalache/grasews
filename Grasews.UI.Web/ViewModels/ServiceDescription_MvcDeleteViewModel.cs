using System.ComponentModel.DataAnnotations;

namespace Grasews.Web.ViewModels
{
    public class ServiceDescription_MvcDeleteViewModel : BaseMvcModel<int>
    {
        [Display(Name = "ServiceName", ResourceType = typeof(WebResource))]
        public string ServiceName { get; set; }

        public int IdOwnerUser { get; set; }
    }
}