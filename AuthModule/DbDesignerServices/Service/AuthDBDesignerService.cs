using AuthModule.Context;
using AuthModule.DbDesignerServices.IService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthModule.DbDesignerServices.Service
{
    public class AuthDBDesignerService: IAuthDBDesignerService
    {
        private readonly AuthContextDB _authContextDb;
        public AuthDBDesignerService(AuthContextDB authContextDb)
        {
            _authContextDb = authContextDb;
        }

        public async Task BuildAuthModuleDataBase()
        {
            await _authContextDb.Database.MigrateAsync();
        }
    }
}
