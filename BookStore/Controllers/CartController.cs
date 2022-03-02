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
    public class CartController : ControllerBase
    {
        public ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [HttpPost]
        [Route("Addintocart")]

        public IActionResult AddCart([FromBody] CartModel cartmodel)
        {
            try
            {
                string result = this.cartBL.AddCart(cartmodel);
                if (result.Equals("Book added into CART"))
                {
                    return this.Ok(new UserResponse<string> { status = true, Message = result });
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

        [HttpPut]
        [Route("UpdateCart")]
        public IActionResult UpdateCart(CartModel cartmodel)
        {

            try
            {
                var result = cartBL.UpdateCart(cartmodel);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Updated CART" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Sorry,not Updated" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
        [Route("DeleteCart")]
        public IActionResult DeleteCart(long CartId)
        {
            try
            {
                if (cartBL.DeleteCart(CartId))
                {
                    return this.Ok(new { Success = true, message = " removed from cart" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Deletion Fail" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("GetAllCart")]
        public List<CartModel> GetAllCart()
        {
            try
            {
                return cartBL.GetAllCart();
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
