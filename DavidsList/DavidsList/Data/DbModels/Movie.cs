namespace DavidsList.Data.DbModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Movie
    {
        [Key]
        [Required]
        public int Id { get; init; }

        [Required]
        public string MoviePath { get; init; }

        public IEnumerable<Comment> Comments { get; init; } = new List<Comment>();

    }
}
