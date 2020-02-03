using System;

namespace Grasews.Web.ViewModels
{
    public class Task_Comment_MvcViewModel : BaseMvcModel<int>
    {
        public string Comment { get; set; }
        public int IdTask { get; set; }
        public int IdOwnerUser { get; set; }
        public string OwnerUserEmail { get; set; }
        public string OwnerUsername { get; set; }
        public DateTime RegistrationDateTime { get; set; }
    }
}