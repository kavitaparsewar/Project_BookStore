using Common_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interfaces
{
    public interface IUserBL
    {
        public UserRegistration Registration(UserRegistration user);
        public UserLogin Login(UserLogin login);
        public string GenerateJWTToken(string email);
        public string ForgetPassword(string EmailId);
        public bool ResetPassword(string EmailId, string password, string confirmpassword);
    }
}


//password for Ram user is 12345