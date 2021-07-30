namespace DavidsList.Models.FormModels
{
    using System.ComponentModel.DataAnnotations;
    public class LoginFormModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
