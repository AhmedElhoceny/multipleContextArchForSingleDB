using Microsoft.AspNetCore.Mvc;
using OrderModule.DbDesignerServices.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderModule.Controllers
{
    public class OrderDataBaseDesignerController : ControllerBase
    {
        private readonly IOrderDBDesignerService _orderDBDesignerService;
        public OrderDataBaseDesignerController(IOrderDBDesignerService orderDBDesignerService)
        {
            _orderDBDesignerService = orderDBDesignerService;
        }
        [HttpPut("buildOrderDataBase")]
        public async Task<IActionResult> BuildOrderDataBase()
        {
            await _orderDBDesignerService.BuildOrderModuleDataBase();
            return Ok();
        }
    }
}
