
namespace DavidsList.Services
{
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;

    public class UserService : IUserService
    {

        private readonly IHttpContextAccessor accessor;

        public UserService(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public ClaimsPrincipal GetUser()
        {
            return accessor?.HttpContext?.User as ClaimsPrincipal;
        }
    }
}
