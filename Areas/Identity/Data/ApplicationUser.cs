using Microsoft.AspNetCore.Identity;

namespace Transportathon.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {

        public string NameSurname { get; set; }
        public string Role { get; set; }
        
    }
}
