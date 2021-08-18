using System.Security.Claims;

namespace DavidsList.Test
{
    public class SharedMethods
    {
        public SharedMethods()
        {

        }
        public  ClaimsPrincipal SimulateAuthentitactedUser()
        {
            return new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, "SomeValueHere"),
                                        new Claim(ClaimTypes.Name, "gunnar@somecompany.com")
                                   }, "TestAuthentication"));
        }

    }
}
