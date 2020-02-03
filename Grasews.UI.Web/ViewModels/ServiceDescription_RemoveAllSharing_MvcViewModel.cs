using System.ComponentModel.DataAnnotations;

namespace Grasews.Web.ViewModels
{
    public class ServiceDescription_RemoveAllSharing_MvcViewModel : BaseMvcModel<int>
    {
        public int IdOwnerUser { get; set; }

        public string IdServiceDescription { get; set; }

        [Display(Name = "ServiceName", ResourceType = typeof(WebResource))]
        public string ServiceName { get; set; }
    }
}