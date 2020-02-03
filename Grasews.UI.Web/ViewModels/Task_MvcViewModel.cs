using System;
using System.Collections.Generic;

namespace Grasews.Web.ViewModels
{
    public class Task_MvcViewModel : BaseMvcModel<int>
    {
        public ICollection<Task_Comment_MvcViewModel> Comments { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
        public int IdOwnerUser { get; set; }
        public int IdServiceDescription { get; set; }
        public DateTime RegistrationDateTime { get; set; }
    }
}