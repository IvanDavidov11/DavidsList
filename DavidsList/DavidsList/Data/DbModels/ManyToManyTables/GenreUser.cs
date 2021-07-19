namespace DavidsList.Data.DbModels.ManyToManyTables
{
    using System.ComponentModel.DataAnnotations;

    public class GenreUser
    {
        [Key]
        [Required]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        [Key]
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
