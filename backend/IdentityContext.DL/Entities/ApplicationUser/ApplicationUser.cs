using Microsoft.AspNetCore.Identity;

namespace IdentityContext.DL.Entities.ApplicationUser
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public Guid? UserId { get; set; }

        public ApplicationUser() : base() { }
    }
}
