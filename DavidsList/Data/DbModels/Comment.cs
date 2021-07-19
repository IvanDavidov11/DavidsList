namespace DavidsList.Data.DbModels
{
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TextContent { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}