namespace DavidsList.Data.DbModels
{
    using Microsoft.AspNetCore.Identity;
    public class User : IdentityUser
    {
        public string Introduction { get; set; }
        public string ProfilePictureUrl { get; set; }

    }
}
