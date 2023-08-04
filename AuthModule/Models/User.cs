using SharedHelpers.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthModule.Models
{
    [Table("Auth_Users")]
    public class User: GeneralEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmaed { get; set; }
        public string Phone { get; set; }
        public string? Address { get; set; }
        public string? NationalId { get; set; }
        public string? ImagePath { get; set; }
        public string PassWord { get; set; }
        public string? EmailVerificationCode { get; set; }
        public bool IsSuperAdmin { get; set; } = false;
        public bool IsAdmin { get; set; } = false;
    }
}
