namespace DavidsList.Services
{
    using System.Security.Claims;

    public interface IUserService
    {
        public ClaimsPrincipal GetUser();
    }
}
