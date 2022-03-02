using Common_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interfaces
{
    public interface ICartRL
    {
        public string AddCart(CartModel cartmodel);
        public string UpdateCart(CartModel cartmodel);
        public bool DeleteCart(long CartId);
        public List<CartModel> GetAllCart();
    }
}
