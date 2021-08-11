namespace DavidsList.Data.DbModels.ManyToManyTables
{
    using DavidsList.Data.DbModels;
    using System.ComponentModel.DataAnnotations;

    public class DislikedMovie
    {
        [Key]
        [Required]

        public string MovieId { get; set; }
        public Movie Movie { get; set; }

        [Key]
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

    }
}
