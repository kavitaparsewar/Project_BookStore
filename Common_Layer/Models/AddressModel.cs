using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.Models
{
    public class AddressModel
    {
        public int AddId { get; set; }
        public int UserId { get; set; }
        public string FullAddress {get; set;}
        public string City { get; set; }
        public string State { get; set; }
        public int AddTId { get; set; }



    }
}
