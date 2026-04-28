namespace Udemy.DatingApp.Web.Entity
{
    public class AppUser
    {
        public required string Id { get; set; } = Guid.NewGuid().ToString();
        public required string UserName { get; set; }
        public required string Email { get; set; }

    }
}
