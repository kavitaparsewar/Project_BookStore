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
        public string GenerateJWTToken(string email)
        {
            try
            {
                return userRL.GenerateJwtToken(email);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string ForgetPassword(string EmailId)
        {
            try
            {
                return userRL.ForgetPassword(EmailId);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ResetPassword(string EmailId, string password, string confirmpassword)
        {
            try
            {
                return userRL.ResetPassword(EmailId, password, confirmpassword);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
