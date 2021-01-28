using eShop.UseCases.PluginInterfaces.StateStore;
using eShop.UseCases.PluginInterfaces.UI;
using System.Threading.Tasks;

namespace eShop.StateStore.DI
{
    public class ShoppingCartStateStore : StateStoreBase, IShoppingCartStateStore
    {
        private readonly IShoppingCart _shoppingCart;

        public ShoppingCartStateStore(IShoppingCart shoppingCard)
        {
            _shoppingCart = shoppingCard;
        }

        public async Task<int> GetItmesCount()
        {
            var order = await _shoppingCart.GetOrderAsync();
            if (order != null && order.LineItems != null && order.LineItems.Count > 0)
                return order.LineItems.Count;

            return 0;
        }

        public void UpdateLineItemsCount()
        {
            BroadCastStateChange();
        }
    }
}
