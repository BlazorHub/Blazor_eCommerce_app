﻿using eShop.CoreBusiness.Models;
using System.Threading.Tasks;

namespace eShop.UseCases.PluginInterfaces.UI
{
    public interface IShoppingCard
    {
        Task<Order> GetOrderAsync();
        Task<Order> AddProductAsync(Product product);
        Task<Order> UpdateQuantityAsync(int productId, int quantity);
        Task<Order> UpdateOrderAsync(Order order);
        Task<Order> DeleteProductAsync(int productId);
        Task<Order> PlaceOrderAsync();
        Task EmptyAsync();
    }
}
