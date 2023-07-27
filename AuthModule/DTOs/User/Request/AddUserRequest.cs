using System.ComponentModel.DataAnnotations;

namespace AuthModule.DTOs.User.Request
{
    public class AddUserRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string? Address { get; set; }
        public string? NationalId { get; set; }
        public string? ImagePath { get; set; }
        [Required]
        public string PassWord { get; set; }
    }
}
