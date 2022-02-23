using Common_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interfaces
{
    public interface IUserRL
    {
        public UserRegistration Registration(UserRegistration user);
        public UserLogin Login(UserLogin login);
        public string GenerateJwtToken(string email);
        public string ForgetPassword(string EmailId);
        public bool ResetPassword(string EmailId, string password, string confirmpassword);
    }
}
