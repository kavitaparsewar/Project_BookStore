using Business_Layer.Interfaces;
using Common_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {        
            public IUserBL userBL;
            public UserController(IUserBL userBL)
            {
                this.userBL = userBL;
            }
            [HttpPost("Register")]
            public IActionResult AddUser(UserRegistration user)
            {
                try
                {
                    var result = userBL.Registration(user);
                    if (result != null)
                    {
                        return this.Ok(new { Success = true, message = "Registration successfull" });
                    }
                    else
                    {
                        return this.BadRequest(new { Success = false, message = "Registration Unsuccessfull" });
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }

        [HttpPost("Login")]
        public IActionResult Login(UserLogin login)
            
        {
            try
            {
                var result = userBL.Login(login);
                if (result != null)
                {
                    var token = userBL.GenerateJWTToken(login.EmailId);
                    return this.Ok(new { Success = true, message = "login successful" ,token=token});
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "something Wrong" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                string Forget = userBL.ForgetPassword(email);
                if (Forget != null)
                {

                    return this.Ok(new { Success = true, message = "Link for reset password has been sent on your email" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Email does not exist in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
                //throw;
            }
        }

        [Authorize]

        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(string password, string confirmPassword)
        {
            try
            {
                var email = User.Claims.First(e => e.Type == "Email").Value;
                //var email1 = User.FindFirst(ClaimTypes.Email).Value.ToString();
                userBL.ResetPassword(email, password, confirmPassword);
                return Ok(new { message = "Password reset succussfully" });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
