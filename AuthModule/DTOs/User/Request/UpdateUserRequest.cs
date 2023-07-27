using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthModule.DTOs.User.Request
{
    public class UpdateUserRequest: AddUserRequest
    {
        public int Id { get; set; }
    }
}
