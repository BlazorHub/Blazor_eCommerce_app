using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.UI;
using System.Threading.Tasks;

namespace eShop.UseCases.ShoppingCardScreen
{
    public class ViewShoppingCardUseCase : IViewShoppingCardUseCase
    {
        private readonly IShoppingCard _shoppingCard;

        public ViewShoppingCardUseCase(IShoppingCard shoppingCard)
        {
            _shoppingCard = shoppingCard;
        }
        public Task<Order> Execute()
        {
            return _shoppingCard.GetOrderAsync();
        }
    }
}
