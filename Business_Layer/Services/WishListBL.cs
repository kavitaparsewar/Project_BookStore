using Business_Layer.Interfaces;
using Common_Layer.Models;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Services
{
    public class WishListBL : IWishListBL
    {
        IWishListRL wishlistRL;
        public WishListBL(IWishListRL wishlistBL)
        {
            this.wishlistRL = wishlistBL;
        }

        public string AddWishList(WishListModel wishlistmodel)
        {
            try
            {
                return wishlistRL.AddWishList(wishlistmodel);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteWishList(long WishListId)
        {
            try
            {
                if (wishlistRL.DeleteWishList(WishListId))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<WishListModel> GetAllWishList(int UserId)
        {
            try
            {
                return wishlistRL.GetAllWishList(UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
