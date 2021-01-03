using eShop.UseCases.PluginInterfaces.DataStore;
using eShop.UseCases.PluginInterfaces.StateStore;
using eShop.UseCases.PluginInterfaces.UI;

namespace eShop.UseCases.ViewProductScreen
{
    public class AddProductToCardUseCase : IAddProductToCardUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCard _shoppingCard;
        private readonly IShoppingCardStateStore _shoppingCardStateStore;

        public AddProductToCardUseCase(IProductRepository productRepository,
                                       IShoppingCard shoppingCard,
                                       IShoppingCardStateStore shoppingCardStateStore)
        {
            _productRepository = productRepository;
            _shoppingCard = shoppingCard;
            _shoppingCardStateStore = shoppingCardStateStore;
        }

        public async void Execute(int productId)
        {
            var product = _productRepository.GetProduct(productId);
            await _shoppingCard.AddProductAsync(product);

            _shoppingCardStateStore.UpdateLineItemsCount();
        }
    }
}
