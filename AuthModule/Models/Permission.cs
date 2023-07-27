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
    [Table("Auth_Permissions")]
    public class Permission:GeneralEntity
    {
        public string Name { get; set; }
    }
}
