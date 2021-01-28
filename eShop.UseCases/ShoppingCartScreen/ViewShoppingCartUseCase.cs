﻿using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.UI;
using System.Threading.Tasks;

namespace eShop.UseCases.ShoppingCartScreen
{
    public class ViewShoppingCartUseCase : IViewShoppingCartUseCase
    {
        private readonly IShoppingCart _shoppingCart;

        public ViewShoppingCartUseCase(IShoppingCart shoppingCard)
        {
            _shoppingCart = shoppingCard;
        }
        public Task<Order> Execute()
        {
            return _shoppingCart.GetOrderAsync();
        }
    }
}
