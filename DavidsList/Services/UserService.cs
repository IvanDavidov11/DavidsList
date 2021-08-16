namespace DavidsList.Services
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;
    using DavidsList.Services.Interfaces;
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
