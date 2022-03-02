using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.Models
{
    public class UserResponse <T>
    {
        public bool status { get; set; }
        public string Message { get; set; }
        public T data { get; set; }

        //public int UserId { get; set; }
        //public string FullName { get; set; }
        //public string EmailId { get; set; }
        //public string MobileNumber { get; set; }
        //public string Password { get; set; }
    }
}
