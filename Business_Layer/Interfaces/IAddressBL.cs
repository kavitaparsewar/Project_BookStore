using Common_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interfaces
{
    public interface IAddressBL
    {
        public string AddAddress(AddressModel addressmodel);
        public string UpdateAddress(AddressModel addressmodel);
        List<AddressModel> GetAllAddress();

    }
}
