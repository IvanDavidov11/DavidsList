namespace DavidsList.Data.DbModels
{
    using DavidsList.Data.DbModels;
    using System.ComponentModel.DataAnnotations;

    public class SeenMovie
    {
        [Key]
        [Required]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        [Key]
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
