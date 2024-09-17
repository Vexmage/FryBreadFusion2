namespace FrybreadFusion.ViewModels
{
    public class UserManagementViewModel
    {
        public IEnumerable<UserDetailsViewModel>? Users { get; set; }
    }

    public class UserDetailsViewModel
    {
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public IEnumerable<string>? Roles { get; set; }
    }

}
