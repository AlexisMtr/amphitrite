using Microsoft.AspNetCore.Authorization;

namespace Amphitrite.Attributes
{
    public class MultipleAuthorizeAttribute : AuthorizeAttribute
    {
        public MultipleAuthorizeAttribute(string[] roles)
        {
            Roles = string.Join(',', roles);
        }
    }
}
