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
    public class WishListController : ControllerBase
    {
        public IWishListBL wishlistBL;
        public WishListController(IWishListBL wishlistBL)
        {
            this.wishlistBL = wishlistBL;
        }

        [HttpPost]
        [Route("AddintoWishList")]

        public IActionResult AddWishList([FromBody] WishListModel wishlistmodel)
        {
            try
            {
                string result = this.wishlistBL.AddWishList(wishlistmodel);
                if (result.Equals("Book Added successfully"))
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

                return this.NotFound(new { status=false, Message=ex.Message});
            }
        }

        [HttpDelete]
        [Route("DeleteWishList")]
        public IActionResult DeleteWishList(long WishListId)
        {
            try
            {
                if (wishlistBL.DeleteWishList(WishListId))
                {
                    return this.Ok(new { Success = true, message = " removed from wishlist" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Deletion of wishlist is Fail" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("GetWishList")]
      
            public IActionResult GetAllWishList(int UserId)
        {
            try
            {
                var result = this.wishlistBL.GetAllWishList(UserId);
                if (result != null)
                {

                    return this.Ok(new { status = true, Message = "Retrieving successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { status = false, Message =  "unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new UserResponse<string>() { status = false, Message = ex.Message });
            }
        }


    }
}
