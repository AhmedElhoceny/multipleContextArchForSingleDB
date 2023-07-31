using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthModule.DTOs.User.Request
{
    public class LoginRequest
    {
        public string email { get; set; }
        public string password { get; set; }
        public int CompanyId { get; set; }
    }
}
