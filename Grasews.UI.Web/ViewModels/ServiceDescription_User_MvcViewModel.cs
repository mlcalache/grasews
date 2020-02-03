using System.ComponentModel;

namespace Grasews.Web.ViewModels
{
    public class ServiceDescription_User_MvcViewModel : BaseMvcModel<int>
    {
        public int IdOwnerUser { get; set; }
        public int IdServiceDescription { get; set; }
        public int IdSharedUser { get; set; }

        [DisplayName("Service Name")]
        public string ServiceName { get; set; }

        [DisplayName("E-mail")]
        public string SharedUserEmail { get; set; }

        [DisplayName("First name")]
        public string SharedUserFirstName { get; set; }

        [DisplayName("Last name")]
        public string SharedUserLastName { get; set; }

        [DisplayName("Username")]
        public string SharedUserUsername { get; set; }
    }
}