using Common_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interfaces
{
   public interface ICartBL
    {
        public string AddCart(CartModel cartmodel);
        public string UpdateCart(CartModel cartmodel);
        public bool DeleteCart(long CartId);
        public List<CartModel> GetAllCart();
    }
}
