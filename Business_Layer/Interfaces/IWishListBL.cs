using Common_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interfaces
{
    public interface IWishListBL
    {
        public string AddWishList(WishListModel wishlistmodel);      
        public bool DeleteWishList(long WishListId);
        public List<WishListModel> GetAllWishList(int UserId);
    }
}
