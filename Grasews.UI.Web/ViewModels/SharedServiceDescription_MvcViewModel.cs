using System.Collections.Generic;

namespace Grasews.Web.ViewModels
{
    public class SharedServiceDescription_MvcViewModel
    {
        public int IdServiceDescription { get; set; }
        public List<ServiceDescription_User_MvcViewModel> ServiceDescription_UserViewModels { get; set; }
    }
}