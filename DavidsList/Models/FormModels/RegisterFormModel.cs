namespace DavidsList.Models.FormModels
{
    using System.ComponentModel.DataAnnotations;
    public class RegisterFormModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
