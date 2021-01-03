using eShop.UseCases.PluginInterfaces.StateStore;
using eShop.UseCases.PluginInterfaces.UI;
using System.Threading.Tasks;

namespace eShop.StateStore.DI
{
    public class ShoppingCardStateStore : StateStoreBase, IShoppingCardStateStore
    {
        private readonly IShoppingCard _shoppingCard;

        public ShoppingCardStateStore(IShoppingCard shoppingCard)
        {
            _shoppingCard = shoppingCard;
        }

        public async Task<int> GetItmesCount()
        {
            var order = await _shoppingCard.GetOrderAsync();
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
