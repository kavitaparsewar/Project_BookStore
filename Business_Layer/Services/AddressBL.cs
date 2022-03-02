using Business_Layer.Interfaces;
using Common_Layer.Models;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Services
{
    public class AddressBL : IAddressBL
    {
        IAddressRL addressRL;
        public AddressBL(IAddressRL addressBL)
        {
            this.addressRL = addressBL;
        }

        public string AddAddress(AddressModel addressmodel)
        {
            try
            {
                return addressRL.AddAddress(addressmodel);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public string UpdateAddress(AddressModel addressmodel)
        {
            try
            {
                return addressRL.UpdateAddress(addressmodel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<AddressModel> GetAllAddress()
        {
            try
            {
                return this.addressRL.GetAllAddress();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
