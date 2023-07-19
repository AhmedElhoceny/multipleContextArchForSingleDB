using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using OrderModule.Context;
using OrderModule.DbDesignerServices.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderModule.DbDesignerServices.Service
{
    public class OrderDBDesignerService : IOrderDBDesignerService
    {
        private readonly OrderContextDb _orderContextDb;
        public OrderDBDesignerService(OrderContextDb orderContextDb)
        {
            _orderContextDb = orderContextDb;
        }

        public async Task BuildOrderModuleDataBase()
        {
            await _orderContextDb.Database.MigrateAsync();
        }
    }
}
