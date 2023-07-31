using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthModule.DTOs.Permissions.Request
{
    public class ModuleCompanyRequest
    {
        public List<string> Modules { get; set; }
        public int CompanyId { get; set; }
    }
}
