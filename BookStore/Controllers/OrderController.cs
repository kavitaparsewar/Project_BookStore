using Business_Layer.Interfaces;
using Common_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public IOrderBL orderBL;
        public OrderController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }

        [HttpPost]
        [Route("AddOrder")]

        public IActionResult AddOrders([FromBody] OrderModel ordermodel)
        {
            try
            {
                string result = this.orderBL.AddOrders(ordermodel);
                if (result.Equals("Order submitted"))
                {
                    return this.Ok(new UserResponse<string>() { status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("GetOrder")]
        public IActionResult GetOrder(int UserId)
        {
            try
            {
                var result = this.orderBL.GetOrder(UserId);
                if (result != null)
                {
                    return this.Ok(new { status = true, Message = "retrieveD successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new UserResponse<string>() { status = false, Message = "Retrieval unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new UserResponse<string>() { status = false, Message = ex.Message });
            }
        }

    }
}
