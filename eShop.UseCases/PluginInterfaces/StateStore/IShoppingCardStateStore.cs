using System.Threading.Tasks;

namespace eShop.UseCases.PluginInterfaces.StateStore
{
    public interface IShoppingCardStateStore : IStateStore
    {
        Task<int> GetItmesCount();

        void UpdateLineItemsCount();
    }
}
