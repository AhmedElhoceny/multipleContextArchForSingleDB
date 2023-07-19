using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthModule.DbDesignerServices.IService
{
    public interface IAuthDBDesignerService
    {
        Task BuildAuthModuleDataBase();
    }
}
