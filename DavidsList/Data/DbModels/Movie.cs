namespace DavidsList.Data.DbModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Movie
    {
        [Key]
        public string Id { get; init; }

        [Required]
        public string MoviePath { get; init; }

        [Required]
        public string Img { get; init; }
        [Required]
        public string Title { get; init; }

        [Required]
        public string ReleaseDate { get; init; }

        public IEnumerable<Comment> Comments { get; init; } = new List<Comment>();

    }
}
