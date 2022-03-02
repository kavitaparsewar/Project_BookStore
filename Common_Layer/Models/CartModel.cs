using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.Models
{
    public class CartModel
    {       
        public int  CartId { get; set; }
        public int  BookId { get; set; }
        public int  UserId { get; set; }
        public int CartQuantity { get; set; }
        public BookModel bookModel { get; set; }
    }

}
