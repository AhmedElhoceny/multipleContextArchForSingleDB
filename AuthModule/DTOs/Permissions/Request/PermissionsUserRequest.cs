using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthModule.DTOs.Permissions.Request
{
    public class PermissionsUserRequest
    {
        public List<string> Permissions { get; set; }
        public int UserId { get; set; }
    }
}
