using eShop.CoreBusiness.Models;
using System.Threading.Tasks;

namespace eShop.UseCases.ShoppingCardScreen
{
    public interface IViewShoppingCardUseCase
    {
        Task<Order> Execute();
    }
}