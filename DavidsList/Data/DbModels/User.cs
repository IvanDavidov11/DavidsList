namespace DavidsList.Data.DbModels
{
    using DavidsList.Data.DbModels;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;
    public class User
    {
        [Key]
        [Required]
        public int Id { get; init; }

        [Required]
        [MaxLength(UsernameMaxLength)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
