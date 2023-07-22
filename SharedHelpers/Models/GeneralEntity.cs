using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHelpers.Models
{
    public class GeneralEntity
    {
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
