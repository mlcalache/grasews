namespace Grasews.Web.ViewModels
{
    public class ShareInvitation_Rejection_MvcViewModel : BaseMvcModel<int>
    {
        public string Email { get; set; }
        public int IdServiceDescription { get; set; }
        public int IdUserInviter { get; set; }
    }
}