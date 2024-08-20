using Microsoft.AspNetCore.Identity;

namespace Practice_web_api.IdentityModel
{
    public class ApplicationUser : IdentityUser
    {
        public UserProfile UserProfile { get; set; }
    }
}
