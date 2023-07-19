using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthModule.Models
{
    [Table("Auth_Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
