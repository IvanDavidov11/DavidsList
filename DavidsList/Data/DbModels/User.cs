namespace DavidsList.Data.DbModels
{
    using DavidsList.Data.DbModels.ManyToManyTables;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public string Introduction { get; set; }
        public string ProfilePictureUrl { get; set; }

        public IEnumerable<GenreUser> UserGenres { get; set; } = new HashSet<GenreUser>();

    }
}
