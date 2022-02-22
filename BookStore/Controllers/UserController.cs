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

                    return this.Ok(new { Success = true, message = "login successful" });
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

        }
    }
