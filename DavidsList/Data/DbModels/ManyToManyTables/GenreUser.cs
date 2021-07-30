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
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
