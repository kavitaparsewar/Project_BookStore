using Common_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interfaces
{
    public interface IOrderBL
    {
        public string AddOrders(OrderModel ordermodel);
        public List<OrderModel> GetOrder(int UserId);

    }
}
