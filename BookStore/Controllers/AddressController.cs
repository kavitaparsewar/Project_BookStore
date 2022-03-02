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
    public class AddressController : ControllerBase
    {
        public IAddressBL addressBL;
        public AddressController(IAddressBL addressBL)
        {
            this.addressBL = addressBL;
        }

        [HttpPost]
        [Route("Addaddress")]
        public IActionResult AddAddress([FromBody] AddressModel addressmodel)
        {
            try
            {
                string result = this.addressBL.AddAddress(addressmodel);
                if (result.Equals("Address Added succssfully"))
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
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("UpdateAddress")]
        public IActionResult UpdateAddress(AddressModel addressmodel)
        {

            try
            {
                var result = addressBL.UpdateAddress(addressmodel);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Updated Address successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Sorry,address not Updated" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("GetAllAddress")]
        //public List<AddressModel> GetAllAddress()
             public IActionResult GetAllAddresses()
        {
            try
            {
                var result = this.addressBL.GetAllAddress();
                if (result != null)
                {
                    return this.Ok(new UserResponse<string>() { status = true, Message = "Retrieval all addresses succssful"});
                }
                else
                {
                    return this.BadRequest(new UserResponse<string>() { status = false, Message = "Retrieval is unsucessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new UserResponse<string>() { status = false, Message = ex.Message });
            }
        }

    }
}
