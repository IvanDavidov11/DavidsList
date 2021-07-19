namespace DavidsList.Data.DbModels
{
    using System.ComponentModel.DataAnnotations;
    public class Genre
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string GenreType { get; set; }

        [Required]
        public string ApiPath { get; set; }
    }
}
