using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthModule.DTOs.User.Request
{
    public class UserTokenInfo
    {
        public int userId { get; set; }
        public int companyId { get; set; }
        public List<string> Permissions { get; set; }
    }
}
