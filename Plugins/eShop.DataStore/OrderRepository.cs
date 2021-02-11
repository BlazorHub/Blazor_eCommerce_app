﻿using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.DataStore;
using System.Collections.Generic;
using System.Linq;

namespace eShop.DataStore
{
    public class OrderRepository : IOrderRepository
    {
        private Dictionary<int, Order> orders;

        public OrderRepository()
        {
            orders = new Dictionary<int, Order>();
        }

        public int CreateOrder(Order order)
        {

            order.OrderId = orders.Count + 1;            
            orders.Add(order.OrderId.Value, order);
            return order.OrderId.Value;             // return the id of the order 
        }

        public IEnumerable<OrderLineItem> GetLineItemsByOrderId(int orderId)
        {
            throw new System.NotImplementedException();
        }

        public Order GetOrder(int id)
        {
            throw new System.NotImplementedException();
        }

        public Order GetOrderByUniqueId(string uniqueId)
        {
            // if we use List we need to convert the Dictionary into the List, and that takes time and resources            

            //foreach (var order in orders.Values.ToList())
            //    if (order.UniqueId == uniqueId)
            //        return order;

            foreach (var order in orders)              // look in all the orders
                if (order.Value.UniqueId == uniqueId)  // and find the order with the UniqueKey
                    return order.Value;                // order is of type Order

            return null;
        }

        public IEnumerable<Order> GetOrders()
        {
            return orders.Values;
        }

        public IEnumerable<Order> GetOutstadingOrders()
        {
            // convert from Dictionary<int, Order> to a List<Order>

            var allOrders = (IEnumerable<Order>)orders.Values;
            return allOrders.Where(x => x.DateProcessed.HasValue == false); // is NULL or has Value
        }

        public IEnumerable<Order> GetProcessedOrders()
        {
            var allOrders = (IEnumerable<Order>)orders.Values;
            return allOrders.Where(x => x.DateProcessed.HasValue);
        }

        public void UpdateOrder(Order order)
        {
            // orders[order.OrderId.Value] == null   ??? orders in the database ???
            // !order.OrderId.HasValue              : the order ID has no value but the other can have fields have
            // orders[order.OrderId.Value] == null  : all the fields are null, the object is not instantiated

            if (order == null || !order.OrderId.HasValue || orders[order.OrderId.Value] == null) 
                return;

            orders[order.OrderId.Value] = order;
        }
    }
}
