using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int AddId { get; set; }
        public int BookId { get; set; }
        public int TotalPrice { get; set; }
        public int QuantityToBuy { get; set; }
        public BookModel bookmodel { get; set; }

    }
}
