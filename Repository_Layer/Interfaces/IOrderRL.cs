using Common_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interfaces
{
    public interface IOrderRL
    {
        public string AddOrders(OrderModel ordermodel);
        public List<OrderModel> GetOrder(int UserId);
    }
}
