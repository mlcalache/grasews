using System.ComponentModel.DataAnnotations;

namespace Grasews.Web.ViewModels
{
    public class ServiceDescription_RemoveSharing_MvcViewModel : BaseMvcModel<int>
    {
        public int IdOwnerUser { get; set; }

        public int IdServiceDescription { get; set; }

        public int IdServiceDescription_User { get; set; }

        [Display(Name = "ServiceName", ResourceType = typeof(WebResource))]
        public string ServiceName { get; set; }

        [Display(Name = "Email", ResourceType = typeof(WebResource))]
        public string SharedUserEmail { get; set; }
    }
}