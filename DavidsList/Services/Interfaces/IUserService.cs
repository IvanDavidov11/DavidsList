namespace DavidsList.Services.Interfaces
{
    using System.Security.Claims;

    public interface IUserService
    {
        public ClaimsPrincipal GetUser();
    }
}
