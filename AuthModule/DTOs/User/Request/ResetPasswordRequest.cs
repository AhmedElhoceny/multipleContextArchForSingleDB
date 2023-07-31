using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthModule.DTOs.User.Request
{
    public class ResetPasswordRequest
    {
        public string email { get; set; }
        public string verificationCode { get; set; }
        public string newPassword { get; set; }
    }
}
