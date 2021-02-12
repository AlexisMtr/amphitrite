using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Amphitrite.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<UserPoolAssociation> Pools { get; set; }
    }
}
