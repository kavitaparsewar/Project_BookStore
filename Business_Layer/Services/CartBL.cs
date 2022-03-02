using Business_Layer.Interfaces;
using Common_Layer.Models;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Services
{
    public class CartBL : ICartBL
    {
        ICartRL cartRL;
        public CartBL(ICartRL cartBL)
        {
            this.cartRL = cartBL;
        }

        public string AddCart(CartModel cartmodel)
        {
            try
            {
                return cartRL.AddCart(cartmodel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string UpdateCart(CartModel cartmodel)
        {
            try
            {
                return cartRL.UpdateCart(cartmodel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteCart(long CartId)
        {
            try
            {
                if (cartRL.DeleteCart(CartId))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<CartModel> GetAllCart()
        {
            try
            {
                return cartRL.GetAllCart();
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
