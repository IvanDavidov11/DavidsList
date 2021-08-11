namespace DavidsList.Data.DbModels
{
    using DavidsList.Data.DbModels.ManyToManyTables;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public string Introduction { get; set; }
        public string ProfilePictureUrl { get; set; }

        public ICollection<GenreUser> UserGenres { get; set; } = new HashSet<GenreUser>();
        public ICollection<SeenMovie> SeenMovies{ get; set; } = new HashSet<SeenMovie>();
        public ICollection<LikedMovie> LikedMovies { get; set; } = new HashSet<LikedMovie>();
        public ICollection<DislikedMovie> DislikedMovies { get; set; } = new HashSet<DislikedMovie>();
        public ICollection<FavouritedMovie> FavouritedMovies { get; set; } = new HashSet<FavouritedMovie>();
        public ICollection<FlaggedMovie> FlaggedMovies { get; set; } = new HashSet<FlaggedMovie>();

    }
}
