namespace Grasews.Web.DTOs
{
    public class LoginRequestDTO
    {
        public string username { get; set; }
        public string password { get; set; }
        public string grant_type { get; set; }
    }
}