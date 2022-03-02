using Common_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interfaces
{
    public interface IAddressRL
    {
        public string AddAddress(AddressModel addressmodel);
        public string UpdateAddress(AddressModel addressmodel);
        List<AddressModel> GetAllAddress();
    }
}
