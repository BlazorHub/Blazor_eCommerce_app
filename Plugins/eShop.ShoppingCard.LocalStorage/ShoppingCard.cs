using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.DataStore;
using eShop.UseCases.PluginInterfaces.UI;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.ShoppingCard.LocalStorage
{
    public class ShoppingCard : IShoppingCard
    {
        private const string cstrShoppingCard = "eShop.ShoppingCard";

        private readonly IJSRuntime _jSRuntime;
        private readonly IProductRepository _productRepository;

        public ShoppingCard(IJSRuntime jSRuntime, IProductRepository productRepository)
        {
            _jSRuntime = jSRuntime;
            _productRepository = productRepository;
        }

        public async Task<Order> AddProductAsync(Product product)
        {
            // get the Product from localStorage         
            var order = await GetOrder();

            // add the Product
            order.AddProduct(product.ProductId, 1, product.Price);

            // set the Order to the localStorage
            await SetOrder(order);

            return order;
        }

        public async Task<Order> DeleteProductAsync(int productId)
        {
            var order = await GetOrder();
            order.RemoveProduct(productId);
            await SetOrder(order);

            return order;
        }

        public Task EmptyAsync()
        {
            // will stay in the same Key
            return SetOrder(null);
        }

        public async Task<Order> GetOrderAsync()
        {
            return await GetOrder();
        }

        public Task<Order> PlaceOrderAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            await SetOrder(order);
            return order;
        }

        public async Task<Order> UpdateQuantityAsync(int productId, int quantity)
        {
            var order = await GetOrder();
            if (quantity < 0)
                return order;
            else if (quantity == 0)
                return await DeleteProductAsync(productId);

            var lineItem = order.LineItems.SingleOrDefault(x => x.ProductId == productId);
            if (lineItem != null) lineItem.Quantity = quantity;
            await SetOrder(order);

            return order;
        }

        private async Task<Order> GetOrder()
        {
            Order order;

            var strOrder = await _jSRuntime.InvokeAsync<string>("localStorage.getItem", cstrShoppingCard);
            if (!string.IsNullOrWhiteSpace(strOrder) && strOrder.ToLower() != "null")
                order = JsonConvert.DeserializeObject<Order>(strOrder);
            else
            {
                order = new Order();
                await SetOrder(order);
            }

            foreach (var item in order.LineItems)
            {   // items that are hardcoded in the ProductRepository
                item.Product = _productRepository.GetProduct(item.ProductId);
            }

            return order;
        }

        private async Task SetOrder(Order order)
        {
            await _jSRuntime.InvokeVoidAsync("localStorage.setItem",
                                             cstrShoppingCard,
                                             JsonConvert.SerializeObject(order));
        }

    }
}
