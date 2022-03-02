using Common_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interfaces
{
    public interface IWishListRL
    {
        public string AddWishList(WishListModel wishlistmodel);
        public bool DeleteWishList(long WishListId);
        public List<WishListModel> GetAllWishList(int UserId);
    }
}
