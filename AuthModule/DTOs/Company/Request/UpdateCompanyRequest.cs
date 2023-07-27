using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthModule.DTOs.Company.Request
{
    public class UpdateCompanyRequest: AddCompanyRequest
    {
        public int Id { get; set; }
    }
}
