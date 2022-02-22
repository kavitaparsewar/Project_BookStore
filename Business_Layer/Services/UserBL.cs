using Business_Layer.Interfaces;
using Common_Layer.Models;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;
        public UserBL(IUserRL userBL)
        {
            this.userRL = userBL;
        }
        public UserRegistration Registration(UserRegistration user)
        {
            try
            {
                return userRL.Registration(user);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public UserLogin Login(UserLogin login)
        {
            try
            {
                return userRL.Login(login);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
