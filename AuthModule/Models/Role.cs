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
    [Table("Auth_Roles")]   
    public class Role:GeneralEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
