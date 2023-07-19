using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderModule.DbDesignerServices.IService
{
    public interface IOrderDBDesignerService
    {
        Task BuildOrderModuleDataBase();
    }
}
