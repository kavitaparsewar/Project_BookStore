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
    }
}
