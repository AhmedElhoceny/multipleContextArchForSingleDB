using SharedHelpers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthModule.Models
{
    [Table("Auth_UserPermissions")]
    public class UserPermission:GeneralEntity
    {
        [Key]
        public int Id { get; set; }
        public int User_Id { get; set; }
        [ForeignKey(nameof(User_Id))]
        public User User { get; set; }
        public int Permission_Id { get; set; }
        [ForeignKey(nameof(Permission_Id))]
        public Permission Permission { get; set; }
    }
}
