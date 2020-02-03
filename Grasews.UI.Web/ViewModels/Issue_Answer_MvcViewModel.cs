using System;

namespace Grasews.Web.ViewModels
{
    public class Issue_Answer_MvcViewModel : BaseMvcModel<int>
    {
        public string Answer { get; set; }
        public int IdIssue { get; set; }
        public int IdOwnerUser { get; set; }
        public string OwnerUserEmail { get; set; }
        public string OwnerUsername { get; set; }
        public DateTime RegistrationDateTime { get; set; }
    }
}