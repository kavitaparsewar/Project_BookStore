using Business_Layer.Interfaces;
using Common_Layer.Models;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Services
{
    public class OrderBL : IOrderBL
    {
        IOrderRL orderRL;
        public OrderBL(IOrderRL orderBL)
        {
            this.orderRL = orderBL;
        }

        public string AddOrders(OrderModel ordermodel)
        {
            try
            {
                return this.orderRL.AddOrders(ordermodel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<OrderModel> GetOrder(int UserId)
        {
            try
            {
                return this. orderRL.GetOrder(UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
