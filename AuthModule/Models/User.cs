using SharedHelpers.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthModule.Models
{
    [Table("Auth_Users")]
    public class User: GeneralEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmaed { get; set; }
        public string Phone { get; set; }
        public string? Address { get; set; }
        public string? NationalId { get; set; }
        public string? ImagePath { get; set; }
        public string PassWord { get; set; }
    }
}
